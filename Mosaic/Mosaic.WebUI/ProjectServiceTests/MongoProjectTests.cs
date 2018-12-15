using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using ProjectService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectServiceTests
{
    [TestClass]
    public class MongoProjectTests
    {
        Mock<IMongoDatabase> MockMongoDatabase;
        Mock<IMongoCollection<ProjectStructure>> MockMongoCollection;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMongoDatabase = new Mock<IMongoDatabase>();
            MockMongoCollection = new Mock<IMongoCollection<ProjectStructure>>();
            MockMongoDatabase.Setup(x => x.GetCollection<ProjectStructure>("Project", null)).Returns(MockMongoCollection.Object);
        }

        [TestMethod]
        public void ReadProjectReturnsErrorIfIdEmpty()
        {
            var request = new ProjectRequest() { Id = String.Empty };
            var response = new MongoProject().Read(MockMongoDatabase.Object, request);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void InsertSmallFilesReturnsErrorIfIdEmpty()
        {
            var request = new ProjectInsertSmallFilesRequest() { Id = String.Empty };
            var response = new MongoProject().InsertSmallFiles(MockMongoDatabase.Object, request);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void InsertSmallFilesReturnsErrorIfNullSmallImageIds()
        {
            var request = new ProjectInsertSmallFilesRequest() { Id = ObjectId.GenerateNewId().ToString() };
            var response = new MongoProject().InsertSmallFiles(MockMongoDatabase.Object, request);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void InsertLargeFileReturnsErrorIfIdEmpty()
        {
            var request = new ProjectInsertLargeFileRequest() { Id = String.Empty };
            var response = new MongoProject().InsertLargeFile(MockMongoDatabase.Object, request);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void InsertLargeFileReturnsErrorIfNullLargeImageId()
        {
            var request = new ProjectInsertLargeFileRequest() { Id = ObjectId.GenerateNewId().ToString(), Location = "MasterLocation" };
            var response = new MongoProject().InsertLargeFile(MockMongoDatabase.Object, request);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void InsertLargeFileReturnsErrorIfNullMasterLocation()
        {
            var request = new ProjectInsertLargeFileRequest() { Id = ObjectId.GenerateNewId().ToString(), LargeFileId = ObjectId.GenerateNewId().ToString() };
            var response = new MongoProject().InsertLargeFile(MockMongoDatabase.Object, request);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void InsertMosaicFileReturnsErrorIfIdEmpty()
        {
            var request = new ProjectInsertMosaicFileRequest() { Id = String.Empty };
            var response = new MongoProject().InsertMosaicFile(MockMongoDatabase.Object, request);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void InsertMosaicFileReturnsErrorIfNullMosaicLocation()
        {
            var request = new ProjectInsertMosaicFileRequest() { Id = ObjectId.GenerateNewId().ToString() };
            var response = new MongoProject().InsertMosaicFile(MockMongoDatabase.Object, request);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void DeleteProjectReturnsErrorIfIdEmpty()
        {
            var request = new ProjectRequest() { Id = String.Empty };
            var response = new MongoProject().Delete(MockMongoDatabase.Object, request);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }
    }
}
