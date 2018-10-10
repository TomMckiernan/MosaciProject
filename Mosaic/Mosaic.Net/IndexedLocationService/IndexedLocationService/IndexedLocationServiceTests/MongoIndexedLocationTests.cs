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

        [TestMethod]
        public void CanInsertIndexedLocationIfNoneExist()
        {

        }

        [TestMethod]
        public void WillReplaceIndexedLocationIfOneExists()
        {
        }

        [TestMethod]
        public void DefaultIndexedLocationIfNoneExist()
        {

            //Assert.AreEqual("M:\Private" , response.Location);
        }

        [TestMethod]
        public void FirstIndexedLocationReturnedIfMultipleInDatabase()
        {

        }

        [TestMethod]
        public void ErrorReturnedIfNoIndexedLocationExists()
        {
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
