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
            var id = "1";
            var moqProjectResponse = new ProjectResponse() { Project = new ProjectStructure() };

            MockMakerClient.Setup(x => x.ReadImageFileIndex(It.IsAny<string>())).Returns(new ImageFileIndexResponse { });
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(moqProjectResponse);

            var model = new ImageFileIndexModel();
            model.ReadImageFileIndex(MockMakerClient.Object, indexedLocation, id);
            Assert.AreEqual(0, model.Files.Count);
        }

        [TestMethod]
        public void ReadImageFileIndexReturnsOneFilesIfOneInCollection()
        {
            var indexedLocation = "indexedLocation";
            var id = "1";
            var moqResponse = new ImageFileIndexResponse();
            moqResponse.Files.Add(new ImageFileIndexStructure() {
                Id = ObjectId.GenerateNewId().ToString(),
                FileName = "",
                FilePath = "",
                LastWriteTime = DateTime.Now.ToString(),
                Metadata = ""

            });
            var moqProjectResponse = new ProjectResponse() { Project = new ProjectStructure() };

            MockMakerClient.Setup(x => x.ReadImageFileIndex(It.IsAny<string>())).Returns(moqResponse);
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(moqProjectResponse);

            var model = new ImageFileIndexModel();
            model.ReadImageFileIndex(MockMakerClient.Object, indexedLocation, id);
            Assert.AreEqual(1, model.Files.Count);
        }

        [TestMethod]
        public void ReadImageFileIndexReturnsErrorIfProjectDoesNotExist()
        {
            var indexedLocation = "indexedLocation";
            var id = "1";
            var moqProjectResponse = new ProjectResponse() { };

            MockMakerClient.Setup(x => x.ReadImageFileIndex(It.IsAny<string>())).Returns(new ImageFileIndexResponse { });
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(moqProjectResponse);

            var model = new ImageFileIndexModel();
            model.ReadImageFileIndex(MockMakerClient.Object, indexedLocation, id);
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void ReadImageFileIndexReturnsOnlyFilesNotAlreadyImported()
        {

        }
    }
}
