using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mosaic.WebUI.Models;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class IndexedLocationModelTests
    {
        Mock<IMakerClient> MockMakerClient;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMakerClient = new Mock<IMakerClient>();
            MockMakerClient.Setup(x => x.ReadIndexedLocation()).Returns(new IndexedLocationResponse { });
        }

        [TestMethod]
        public void IndexedLocationSetToReturnedValueIfValid()
        {
            var indexedLocation = "Location";
            MockMakerClient.Setup(x => x.ReadIndexedLocation()).Returns(new IndexedLocationResponse { IndexedLocation = indexedLocation});

            var model = new IndexedLocationModel();
            model.RequestIndexedLocation(MockMakerClient.Object);
            Assert.AreEqual(indexedLocation, model.IndexedLocation);     
        }

        [TestMethod]
        public void IndexedLocationNotSetIfErrorReturned()
        {
            var newIndexedLocation = "NewLocation";
            var error = "Error";
            MockMakerClient.Setup(x => x.ReadIndexedLocation()).Returns(new IndexedLocationResponse { IndexedLocation = newIndexedLocation, Error = error});

            var originalIndexedLocation = "OriginalLocation";
            var model = new IndexedLocationModel() { IndexedLocation = originalIndexedLocation};
            model.RequestIndexedLocation(MockMakerClient.Object);
            Assert.AreEqual(originalIndexedLocation, model.IndexedLocation);
            Assert.AreEqual(error, model.Error);
        }

        // Add more invalid paths
        [TestMethod]
        public void IsValidFalseIfIndexedLocationInvalid()
        {
            var model = new IndexedLocationModel();
            Assert.IsFalse(model.IsIndexedLocationValid);
        }

        // Add more valid paths
        [TestMethod]
        public void IsValidTrueIfIndexedLocationValid()
        {
            var model = new IndexedLocationModel() { IndexedLocation = "C:\\"};
            Assert.IsTrue(model.IsIndexedLocationValid);
        }
    }
}
