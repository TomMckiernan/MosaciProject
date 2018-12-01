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
        public void MyTestMethod()
        {
            var Id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel(Id);
            var outputLocation = "outputLocation";

            var projectResponse = new ProjectResponse()
            {
                Project = new ProjectStructure()
                {
                    Id = Id,
                    LargeFileId = ObjectId.GenerateNewId().ToString(),
                }           
            };
            projectResponse.Project.SmallFileIds.Add("1");

            MockMakerClient.Setup(x => x.ReadProject(It.Is<string>(y => y.Equals(Id)))).Returns(projectResponse);

            var tileFiles = new ImageFileIndexResponse();
            tileFiles.Files.Add(new ImageFileIndexStructure() { Id = ObjectId.GenerateNewId().ToString() });
            MockMakerClient.Setup(x => x.ReadAllImageFiles(It.IsAny<List<string>>())).Returns(tileFiles);

            var masterFile = new ImageFileResponse(){ File = new ImageFileIndexStructure() { Id = ObjectId.GenerateNewId().ToString() }};
            MockMakerClient.Setup(x => x.ReadImageFile(It.IsAny<string>())).Returns(new ImageFileResponse { File = new ImageFileIndexStructure() { Id = "0" } });
            MockMakerClient.Setup(x => x.Generate(It.Is<IList<ImageFileIndexStructure>>(y => y.Equals(tileFiles)), It.Is<ImageFileIndexStructure>(p => p.Equals(masterFile.File)))).Returns(new ImageMosaicResponse() { Location = outputLocation});

            var response = model.Generate(MockMakerClient.Object, Id);

            Assert.AreEqual(outputLocation, response.Location);
        }

        [TestMethod]
        public void GenerateReturnsErrorIfIdNullOrEmpty()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel(id);
            var response = model.Generate(MockMakerClient.Object, null);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void GenerateReturnsErrorIfReadProjectReturnsError()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel(id);

            var projectResponse = new ProjectResponse() { Error = "Error"};

            MockMakerClient.Setup(x => x.ReadProject(It.Is<string>(y => y.Equals(id)))).Returns(projectResponse);

            var response = model.Generate(MockMakerClient.Object, id);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void GenerateReturnsErrorIfProjectDoesNotContainAnySmallFiles()
        {
            var id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel(id);

            var projectResponse = new ProjectResponse() { Project = new ProjectStructure() };

            MockMakerClient.Setup(x => x.ReadProject(It.Is<string>(y => y.Equals(id)))).Returns(projectResponse);

            var response = model.Generate(MockMakerClient.Object, id);
            Assert.IsFalse(String.IsNullOrEmpty(response.Error));
        }

        [TestMethod]
        public void GenerateReturnsErrorIfReadAllImageFilesReturnsError()
        {
            Assert.IsFalse(true);
        }

        [TestMethod]
        public void GenerateReturnsErrorIfProjectDoesNotContainALargeFile()
        {
            Assert.IsFalse(true);
        }

        [TestMethod]
        public void GenerateReturnsErrorIfReadImageFileReturnsError()
        {
            Assert.IsFalse(true);
        }
    }
}
