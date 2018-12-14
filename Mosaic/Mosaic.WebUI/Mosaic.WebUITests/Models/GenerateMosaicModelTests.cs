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
    public class GenerateMosaicModelTests
    {
        Mock<IMakerClient> MockMakerClient;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMakerClient = new Mock<IMakerClient>();
            MockMakerClient.Setup(x => x.ReadAllImageFiles(It.IsAny<List<string>>())).Returns(new ImageFileIndexResponse { });
            MockMakerClient.Setup(x => x.ReadImageFile(It.IsAny<string>())).Returns(new ImageFileResponse { File = new ImageFileIndexStructure() { Id = "0"} });
        }

        [TestMethod]
        public void GenerateReturnsErrorIfIdNullOrEmpty()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel();
            var response = model.Generate(MockMakerClient.Object, null);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void GenerateReturnsErrorIfReadProjectReturnsError()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel();

            var projectResponse = new ProjectResponse() { Error = "Error"};

            MockMakerClient.Setup(x => x.ReadProject(It.Is<string>(y => y.Equals(id)))).Returns(projectResponse);

            var response = model.Generate(MockMakerClient.Object, id);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void GenerateReturnsErrorIfProjectDoesNotContainAnySmallFiles()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel();

            var projectResponse = new ProjectResponse() { Project = new ProjectStructure() { Id = id, LargeFileId = ObjectId.GenerateNewId().ToString() } };

            MockMakerClient.Setup(x => x.ReadProject(It.Is<string>(y => y.Equals(id)))).Returns(projectResponse);

            var response = model.Generate(MockMakerClient.Object, id);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void GenerateReturnsErrorIfReadAllImageFilesReturnsError()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel();

            var projectResponse = new ProjectResponse() { Project = new ProjectStructure() { Id = id, LargeFileId = ObjectId.GenerateNewId().ToString() } };
            projectResponse.Project.SmallFileIds.Add("1");

            MockMakerClient.Setup(x => x.ReadProject(It.Is<string>(y => y.Equals(id)))).Returns(projectResponse);
            MockMakerClient.Setup(x => x.ReadAllImageFiles(It.IsAny<IList<string>>())).Returns(new ImageFileIndexResponse() { Error = "Error"});
            MockMakerClient.Setup(x => x.ReadImageFile(It.IsAny<string>())).Returns(new ImageFileResponse(){});

            var response = model.Generate(MockMakerClient.Object, id);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void GenerateReturnsErrorIfProjectDoesNotContainALargeFile()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel();

            var projectResponse = new ProjectResponse() { Project = new ProjectStructure() { Id = id } };
            projectResponse.Project.SmallFileIds.Add("1");

            MockMakerClient.Setup(x => x.ReadProject(It.Is<string>(y => y.Equals(id)))).Returns(projectResponse);

            var response = model.Generate(MockMakerClient.Object, id);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void GenerateReturnsErrorIfReadImageFileReturnsError()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel();

            var projectResponse = new ProjectResponse() { Project = new ProjectStructure() { Id = id, LargeFileId = ObjectId.GenerateNewId().ToString() } };
            projectResponse.Project.SmallFileIds.Add("1");

            MockMakerClient.Setup(x => x.ReadProject(It.Is<string>(y => y.Equals(id)))).Returns(projectResponse);
            MockMakerClient.Setup(x => x.ReadAllImageFiles(It.IsAny<IList<string>>())).Returns(new ImageFileIndexResponse(){});
            MockMakerClient.Setup(x => x.ReadImageFile(It.IsAny<string>())).Returns(new ImageFileResponse() { Error = "Error" });

            var response = model.Generate(MockMakerClient.Object, id);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void ReadProjectDataSetsAllProjectPropertiesCorrectly()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel();

            var projectResponse = new ProjectResponse() { Project = new ProjectStructure() { Id = id, LargeFileId = ObjectId.GenerateNewId().ToString() } };
            projectResponse.Project.SmallFileIds.Add("1");
            MockMakerClient.Setup(x => x.ReadProject(It.Is<string>(y => y.Equals(id)))).Returns(projectResponse);

            model.ReadProjectData(MockMakerClient.Object, id);

            Assert.AreEqual(id, model.ProjectId);
            Assert.AreEqual(1, model.TileImageCount);
        }
    }
}
