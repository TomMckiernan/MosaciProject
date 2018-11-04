using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Moq;
using Mosaic.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class ImageFileIndexModelTests
    {
        Mock<IMakerClient> MockMakerClient;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMakerClient = new Mock<IMakerClient>();
            MockMakerClient.Setup(x => x.ReadImageFileIndex(It.IsAny<string>())).Returns(new ImageFileIndexResponse { });
            //MockMakerClient.Setup(x => x.UpdateImageFileIndex(It.IsAny<string>())).Returns());
        }

        [TestMethod]
        public void ReadImageFileIndexReturnsNoFilesIfNoneInCollection()
        {
            var indexedLocation = "indexedLocation";
            MockMakerClient.Setup(x => x.ReadImageFileIndex(It.IsAny<string>())).Returns(new ImageFileIndexResponse { });

            var model = new ImageFileIndexModel();
            var response = model.ReadImageFileIndex(MockMakerClient.Object, indexedLocation);
            Assert.AreEqual(0, response.Files.Count);
        }


        [TestMethod]
        public void ReadImageFileIndexReturnsOneFilesIfOneInCollection()
        {
            var indexedLocation = "indexedLocation";
            var moqResponse = new ImageFileIndexResponse();
            moqResponse.Files.Add(new ImageFileIndexStructure() {
                Id = ObjectId.GenerateNewId().ToString(),
                FileName = "",
                FilePath = "",
                LastWriteTime = DateTime.Now.ToString(),
                Metadata = ""

            });

            MockMakerClient.Setup(x => x.ReadImageFileIndex(It.IsAny<string>())).Returns(moqResponse);

            var model = new ImageFileIndexModel();
            var response = model.ReadImageFileIndex(MockMakerClient.Object, indexedLocation);
            Assert.AreEqual(1, response.Files.Count);
        }

        //[TestMethod]
        //public void UpdateImageFileIndexReteurnsIndexedLocation()
        //{
        //    var indexedLocation = "indexedLocation";
        //    MockMakerClient.Setup(x => x.UpdateImageFileIndex(It.IsAny<string>())).Returns(new ImageFileIndexUpdateResponse { });

        //    var model = new ImageFileIndexModel();
        //    var response = model.ReadImageFileIndex(MockMakerClient.Object, indexedLocation);
        //    Assert.AreEqual(0, response.Files.Count);
        //}
    }
}
