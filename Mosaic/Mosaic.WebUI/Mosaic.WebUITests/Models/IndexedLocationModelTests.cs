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
        List<string> invalidLocations = new List<string> {
            "", "invalidLocation", "C/"
        };
        List<string> validLocations = new List<string> {
            "C:\\", "C:\\Users\\Tom_m\\OneDrive\\Pictures", "C:\\Users\\Tom_m"
        };

        Mock<IMakerClient> MockMakerClient;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMakerClient = new Mock<IMakerClient>();
            MockMakerClient.Setup(x => x.ReadIndexedLocation()).Returns(new IndexedLocationResponse { });
            //MockMakerClient.Setup(x => x.UpdateIndexedLocation()).Returns(new IndexedLocationResponse { });
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

        [TestMethod]
        public void UpdateIndexedLocationReturnsErrorIfIndexedLocationEmpty()
        {
            var model = new IndexedLocationModel();
            var indexedLocation = String.Empty;
            var response = model.UpdateIndexedLocation(MockMakerClient.Object, indexedLocation);
            Assert.IsTrue(!String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void UpdateIndexedLocationReturnsErrorIfIndexedLocationNull()
        {
            var model = new IndexedLocationModel();
            var response = model.UpdateIndexedLocation(MockMakerClient.Object, null);
            Assert.IsTrue(!String.IsNullOrEmpty(response.Error));
        }

        // Add more invalid paths
        [TestMethod]
        public void IsPathValidFalseIfIndexedLocationInvalid()
        {
            var model = new IndexedLocationModel();
            foreach (var location in invalidLocations)
            {
                model.IndexedLocation = location;
                Assert.IsFalse(model.IsIndexedLocationValid);
            }
        }

        // Add more valid paths
        [TestMethod]
        public void IsPathValidTrueIfIndexedLocationValid()
        {
            var model = new IndexedLocationModel();
            foreach (var location in validLocations)
            {
                model.IndexedLocation = location;
                Assert.IsTrue(model.IsIndexedLocationValid);
            }
        }
    }
}
