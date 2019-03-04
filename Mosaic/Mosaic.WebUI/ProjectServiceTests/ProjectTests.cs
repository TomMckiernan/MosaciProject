using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectServiceTests
{
    [TestClass]
    public class ProjectTests
    {
        MongoClient client;
        IMongoDatabase database;
        Project service;

        private ProjectResponse InsertSmallFilesHelper(Project service, string id)
        {
            var fileIds = new List<string>() { ObjectId.GenerateNewId().ToString(), ObjectId.GenerateNewId().ToString() };
            var request = new ProjectInsertSmallFilesRequest() { Id = id };
            request.SmallFileIds.AddRange(fileIds);
            return service.InsertSmallFiles(request);
        }

        private ProjectResponse InsertLargeFileHelper(Project service, string id)
        {
            var insertRequest = new ProjectInsertLargeFileRequest() { Id = id, LargeFileId = ObjectId.GenerateNewId().ToString(), Location = "//MasterLocation//test.jpg" };
            return service.InsertLargeFile(insertRequest);
        }

        private ProjectResponse InsertMosaicFileHelper(Project service, string id)
        {
            var insertRequest = new ProjectInsertMosaicFileRequest() { Id = id, Location = "//MosaicLocation//test.jpg" };
            return service.InsertMosaicFile(insertRequest);
        }

        private ProjectResponse InsertEdgeFileHelper(Project service, string id)
        {
            var edges = new List<PixelCoordinates>() { new PixelCoordinates() { X = 0, Y = 0}, new PixelCoordinates() { X = 1, Y = 1 } };
            var insertRequest = new ProjectInsertEdgeFileRequest() { Id = id, Location = "//EdgeLocation//test.jpg" };
            insertRequest.Edges.AddRange(edges);
            return service.InsertEdgeFile(insertRequest);
        }

        private ProjectResponse InsertEdgeFileHelper2(Project service, string id)
        {
            var edges = new List<PixelCoordinates>() { new PixelCoordinates() { X = 5, Y = 5 }};
            var insertRequest = new ProjectInsertEdgeFileRequest() { Id = id, Location = "//EdgeLocation//test2.jpg" };
            insertRequest.Edges.AddRange(edges);
            return service.InsertEdgeFile(insertRequest);
        }

        private ProjectResponse ReadProjectHelper(Project service, string id)
        {
            var request = new ProjectRequest() { Id = id };
            return service.ReadProject(request);
        }


        [TestInitialize]
        public void Init()
        {
            client = new MongoClient();
            database = client.GetDatabase("TestMosaicDatabase");
            service = new Project("TestMosaicDatabase");
        }

        // Uses a test directory containing seven jpg images
        [TestMethod]
        public void CreateProjectCreatesNewProjectInCollection()
        {
            var createResponse = service.CreateProject();
            var readResponse = ReadProjectHelper(service, createResponse.Project.Id);

            Assert.AreEqual(createResponse.Project.Id, readResponse.Project.Id);
            Assert.AreEqual(ProjectStructure.Types.State.Created, readResponse.Project.Progress);
        }

        [TestMethod]
        public void InsertSmallFilesInsertsImageIdsIntoProjectAndUpdatesState()
        {
            var createResponse = service.CreateProject();
            // Insert master file first to get project into correct state
            var insertLargeResponse = InsertLargeFileHelper(service, createResponse.Project.Id);
            var insertResponse = InsertSmallFilesHelper(service, createResponse.Project.Id);
            var readResponse = ReadProjectHelper(service, insertResponse.Project.Id);

            Assert.AreEqual(insertResponse.Project.SmallFileIds, readResponse.Project.SmallFileIds);
            Assert.AreEqual(ProjectStructure.Types.State.Smalladded, readResponse.Project.Progress);
        }

        [TestMethod]
        public void InsertSmallFilesInsertsImageIdsAndKeepsExisitingIds()
        {
            var createResponse = service.CreateProject();
            var insertResponse = InsertSmallFilesHelper(service, createResponse.Project.Id);
            var insertResponse2 = InsertSmallFilesHelper(service, createResponse.Project.Id);
            var readResponse = ReadProjectHelper(service, insertResponse.Project.Id);

            // Since InsertSmallFilesHelper inserts two files each
            Assert.AreEqual(4, readResponse.Project.SmallFileIds.Count);
        }

        [TestMethod]
        public void InsertLargeFileInsertsImageIdIntoProjectSetsMasterLocationAndUpdatesState()
        {
            var createResponse = service.CreateProject();
            var insertResponse = InsertLargeFileHelper(service, createResponse.Project.Id);
            var readResponse = ReadProjectHelper(service, insertResponse.Project.Id);

            Assert.AreEqual(insertResponse.Project.LargeFileId, readResponse.Project.LargeFileId);
            Assert.AreEqual(insertResponse.Project.MasterLocation, readResponse.Project.MasterLocation);
            Assert.AreEqual(ProjectStructure.Types.State.Largeadded, readResponse.Project.Progress);
        }

        [TestMethod]
        public void InsertMosaicFileInsertsLocationIntoProjectAndUpdatesState()
        {
            var createResponse = service.CreateProject();
            var insertResponse = InsertMosaicFileHelper(service, createResponse.Project.Id);
            var readResponse = ReadProjectHelper(service, insertResponse.Project.Id);

            Assert.AreEqual(insertResponse.Project.MosaicLocation, readResponse.Project.MosaicLocation);
            Assert.AreEqual(readResponse.Project.Progress, ProjectStructure.Types.State.Completed);
        }

        [TestMethod]
        public void InsertSmallFilesAfterMosaicGenerationInsertsImageIdsIntoProjectButDoesNotUpdateState()
        {
            var createResponse = service.CreateProject();
            var insertMosaicResponse = InsertMosaicFileHelper(service, createResponse.Project.Id);
            var insertResponse = InsertSmallFilesHelper(service, createResponse.Project.Id);
            var readResponse = ReadProjectHelper(service, insertResponse.Project.Id);

            Assert.AreEqual(insertResponse.Project.SmallFileIds, readResponse.Project.SmallFileIds);
            Assert.AreEqual(ProjectStructure.Types.State.Completed, readResponse.Project.Progress);
        }

        [TestMethod]
        public void InsertLargeFileInsertsImageIdIntoProjectSetsMasterLocationButDoesNotUpdateState()
        {
            var createResponse = service.CreateProject();
            var insertMosaicResponse = InsertMosaicFileHelper(service, createResponse.Project.Id);
            var insertResponse = InsertLargeFileHelper(service, createResponse.Project.Id);
            var readResponse = ReadProjectHelper(service, insertResponse.Project.Id);

            Assert.AreEqual(insertResponse.Project.LargeFileId, readResponse.Project.LargeFileId);
            Assert.AreEqual(insertResponse.Project.MasterLocation, readResponse.Project.MasterLocation);
            Assert.AreEqual(ProjectStructure.Types.State.Completed, readResponse.Project.Progress);
        }

        [TestMethod]
        public void DeleteProjectRemovesProjectFromCollection()
        {
            var createResponse = service.CreateProject();
            var request = new ProjectRequest() { Id = createResponse.Project.Id };
            var deleteResponse = service.DeleteProject(request);
            var readResponse = service.ReadProject(request);

            Assert.AreEqual(createResponse.Project.Id, deleteResponse.Project.Id);
            Assert.IsFalse(String.IsNullOrEmpty(readResponse.Error));
        }

        [TestMethod]
        public void ReadAllProjectsReturnsAllProjectsIncludingSmallFileIds()
        {
            var createResponse1 = service.CreateProject();
            var createResponse2 = service.CreateProject();
            var insertResponse1 = InsertSmallFilesHelper(service, createResponse1.Project.Id);
            var insertResponse2 = InsertSmallFilesHelper(service, createResponse2.Project.Id);
            var readAllResponse = service.ReadAllProjects();

            var readResponse1 = readAllResponse.Projects.Where(x => x.Id.Equals(insertResponse1.Project.Id)).First();
            var readResponse2 = readAllResponse.Projects.Where(x => x.Id.Equals(insertResponse2.Project.Id)).First();

            Assert.AreEqual(2, readAllResponse.Projects.Count);
            Assert.AreEqual(insertResponse1.Project.SmallFileIds, readResponse1.SmallFileIds);
            Assert.AreEqual(insertResponse2.Project.SmallFileIds, readResponse2.SmallFileIds);
        }

        [TestMethod]
        public void InsertEdgeFileInsertsLocationAndEdgesListIntoProject()
        {
            var createResponse = service.CreateProject();
            // Insert edge file and edges into project
            var insertResponse = InsertEdgeFileHelper(service, createResponse.Project.Id);
            var readResponse = ReadProjectHelper(service, insertResponse.Project.Id);

            Assert.AreEqual(insertResponse.Project.EdgeLocation, readResponse.Project.EdgeLocation);
            Assert.AreEqual(insertResponse.Project.Edges, readResponse.Project.Edges);
        }

        [TestMethod]
        public void InsertEdgeFileRemovesAllCurrentEdgesBeforeUpdating()
        {
            var createResponse = service.CreateProject();
            // Insert edge file and edges into project
            var insertResponse = InsertEdgeFileHelper(service, createResponse.Project.Id);
            var insertResponse2 = InsertEdgeFileHelper2(service, createResponse.Project.Id);
            var readResponse = ReadProjectHelper(service, insertResponse.Project.Id);

            Assert.AreEqual(insertResponse2.Project.EdgeLocation, readResponse.Project.EdgeLocation);
            Assert.AreEqual(1, readResponse.Project.Edges.Count);
            Assert.AreEqual(insertResponse2.Project.Edges, readResponse.Project.Edges);
        }

        [TestCleanup]
        public void Cleanup()
        {
            database.DropCollection("Project");
        }
    }
}
