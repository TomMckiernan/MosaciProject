using IndexedLocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;

namespace IndexedLocationServiceTests
{
    [TestClass]
    public class MongoIndexedLocationServiceTests
    {
        Mock<IMongoDatabase> MockMongoDatabase;
        Mock<IMongoCollection<IndexedLocationRequest>> MockMongoCollection;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMongoDatabase = new Mock<IMongoDatabase>();
            MockMongoCollection = new Mock<IMongoCollection<IndexedLocationRequest>>();
            MockMongoDatabase.Setup(x => x.GetCollection<IndexedLocationRequest>("IndexedLocation", null)).Returns(MockMongoCollection.Object);
        }

        [TestMethod]
        public void IndexLocationIsValidReturnsLocationAndNoError()
        {
            var location = "TestLocation";
            var request = new IndexedLocationRequest() { IndexedLocation = location };
            var response = new MongoIndexedLocation().Insert(request, MockMongoDatabase.Object);
            Assert.AreEqual(location, response.IndexedLocation);
            Assert.IsTrue(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void IndexedLocationIsNullOrEmptyIndexReturnsError()
        {
            var request = new IndexedLocationRequest() { IndexedLocation = "" };
            var response = new MongoIndexedLocation().Insert(request, MockMongoDatabase.Object);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }
    }
}
