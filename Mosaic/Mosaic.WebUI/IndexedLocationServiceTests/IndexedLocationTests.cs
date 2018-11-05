using IndexedLocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace IndexedLocationServiceTests
{
    [TestClass]
    public class IndexedLocationTests
    {
        MongoClient client;
        IMongoDatabase database;

        [TestInitialize]
        public void Init()
        {
            client = new MongoClient();
            database = client.GetDatabase("TestMosaicDatabase");
        }

        [TestMethod]
        public void NoIndexedLocationInsertsCreatesNewIndexedLocation()
        {
            var indexedLocation = "indexedLocation";
            var service = new IndexedLocation("TestMosaicDatabase");
            var request = new IndexedLocationRequest() { IndexedLocation = indexedLocation };
            service.UpdateIndexedLocation(request);

            var response = service.ReadIndexedLocation();
            Assert.AreEqual(indexedLocation, response.IndexedLocation);
        }

        [TestMethod]
        public void OneIndexedLocationInsertReplacesIndexedLocation()
        {
            var oldIndexedLocation = "oldIndexedLocation";
            var newIndexedLocation = "newIndexedLocation";
            var service = new IndexedLocation("TestMosaicDatabase");

            var request1 = new IndexedLocationRequest() { IndexedLocation = oldIndexedLocation };
            service.UpdateIndexedLocation(request1);

            var request2 = new IndexedLocationRequest() { IndexedLocation = newIndexedLocation };
            service.UpdateIndexedLocation(request2);

            var response = service.ReadIndexedLocation();
            Assert.AreEqual(newIndexedLocation, response.IndexedLocation);
        }

        [TestCleanup]
        public void Cleanup()
        {
            database.DropCollection("IndexedLocation");
        }
    }
}
