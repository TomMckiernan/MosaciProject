using IndexedLocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using System;

namespace IndexedLocationServiceTests
{
    [TestClass]
    public class MongoIndexedLocationServiceTests
    {
        Mock<IMongoDatabase> MockMongoDatabase;
        Mock<IMongoCollection<IndexedLocation>> MockMongoCollection;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMongoDatabase = new Mock<IMongoDatabase>();
            MockMongoCollection = new Mock<IMongoCollection<IndexedLocation>>();
            MockMongoDatabase.Setup(x => x.GetCollection<IndexedLocation>("IndexedLocation", null)).Returns(MockMongoCollection.Object);
        }

        // Integration Test
        [TestMethod]
        public void CanInsertIndexedLocationIfNoneExist()
        {

        }

        // Integration Test
        [TestMethod]
        public void WillReplaceIndexedLocationIfOneExists()
        {
        }

        // Integration Test
        [TestMethod]
        public void DefaultIndexedLocationIfNoneExist()
        {

        }

        // Integration Test
        [TestMethod]
        public void FirstIndexedLocationReturnedIfMultipleInDatabase()
        {

        }

        [TestMethod]
        public void IndexLocationIsValidReturnsLocationAndNoError()
        {
            var location = "TestLocation";
            var request = new IndexedLocation() { Location = location };
            var response = new MongoIndexedLocation().Insert(request, MockMongoDatabase.Object);
            Assert.AreEqual(location, response.Location);
            Assert.IsTrue(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void IndexedLocationIsNullOrEmptyIndexReturnsError()
        {
            var request = new IndexedLocation() { Location = "" };
            var response = new MongoIndexedLocation().Insert(request, MockMongoDatabase.Object);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

    }
}
