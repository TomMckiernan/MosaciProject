using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjectService;
using System;
using System.Collections.Generic;

namespace ProjectServiceTests
{
    [TestClass]
    public class ProjectTests
    {
        MongoClient client;
        IMongoDatabase database;

        [TestInitialize]
        public void Init()
        {
            client = new MongoClient();
            database = client.GetDatabase("TestMosaicDatabase");
        }

        // Uses a test directory containing seven jpg images
        [TestMethod]
        public void CreateProjectCreatesNewProjectInCollection()
        {
            var service = new Project("TestMosaicDatabase");
            var createResponse = service.CreateProject();
            var request = new ProjectRequest(){ Id = createResponse.Project.Id };
            var readResponse = service.ReadProject(request);
            Assert.AreEqual(createResponse.Project.Id, readResponse.Project.Id);
            Assert.AreEqual(ProjectStructure.Types.State.Created, readResponse.Project.Progress);
        }

        [TestMethod]
        public void InsertSmallFilesInsertsImageIdsIntoProjectAndUpdatesState()
        {
            var service = new Project("TestMosaicDatabase");
            var createResponse = service.CreateProject();
            var request = new ProjectInsertSmallFilesRequest() { Id = createResponse.Project.Id };

            var fileIds = new List<string>() { "1", "2" };
            request.SmallFileIds.AddRange(fileIds);

            var insertResponse = service.InsertSmallFiles(request);
            var readRequest = new ProjectRequest() { Id = insertResponse.Project.Id };
            var readResponse = service.ReadProject(readRequest);

            Assert.AreEqual(insertResponse.Project.SmallFileIds, readResponse.Project.SmallFileIds);
            Assert.AreEqual(readResponse.Project.Progress, ProjectStructure.Types.State.Smalladded);
        }

        [TestMethod]
        public void InsertLargeFileInsertsImageIdIntoProjectAndUpdatesState()
        {
            var service = new Project("TestMosaicDatabase");
            var createResponse = service.CreateProject();
            var insertRequest = new ProjectInsertLargeFileRequest() { Id = createResponse.Project.Id, LargeFileId = ObjectId.GenerateNewId().ToString() };
            var insertResponse = service.InsertLargeFile(insertRequest);
            var readRequest = new ProjectRequest() { Id = insertResponse.Project.Id };
            var readResponse = service.ReadProject(readRequest);

            Assert.AreEqual(insertResponse.Project.LargeFileId, readResponse.Project.LargeFileId);
            Assert.AreEqual(readResponse.Project.Progress, ProjectStructure.Types.State.Largeadded);
        }


        [TestMethod]
        public void DeleteProjectRemovesProjectFromCollection()
        {
            var service = new Project("TestMosaicDatabase");
            var createResponse = service.CreateProject();
            var request = new ProjectRequest() { Id = createResponse.Project.Id };
            var deleteResponse = service.DeleteProject(request);
            var readResponse = service.ReadProject(request);

            Assert.AreEqual(createResponse.Project.Id, deleteResponse.Project.Id);
            Assert.IsFalse(String.IsNullOrEmpty(readResponse.Error));
        }

        [TestCleanup]
        public void Cleanup()
        {
            database.DropCollection("Project");
        }
    }
}
