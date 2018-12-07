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
    public class ProjectCardModelTests
    {
        Mock<IMakerClient> MockMakerClient;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMakerClient = new Mock<IMakerClient>();       
        }

        private ProjectStructure CreateProjectStructureHelper(bool SmallAdded, bool LargeAdded, bool Completed)
        {
            var project = new ProjectStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Progress = ProjectStructure.Types.State.Created,
                TimeOfCreation = DateTime.Now.ToString()
            };
            if (SmallAdded)
            {
                project.Progress = ProjectStructure.Types.State.Smalladded;
                var smallTileIds = new List<string>() { "1", "2", "3" };
                project.SmallFileIds.Add(smallTileIds);
            }
            if (LargeAdded)
            {
                project.MosaicLocation = "//MasterLocation//";
                project.LargeFileId = ObjectId.GenerateNewId().ToString();
                project.Progress = ProjectStructure.Types.State.Largeadded;
            }
            if (Completed)
            {
                project.MosaicLocation = "//MosaicLocation//";
                project.Progress = ProjectStructure.Types.State.Completed;
            }
            return project;
        }

        [TestMethod]
        public void ConstructorPopulatesAllPropertiesIfMosaicGenerated()
        {
            var project = CreateProjectStructureHelper(true, true, true);
            var response = new ImageFileResponse(){ File = new ImageFileIndexStructure() {Id = project.LargeFileId, FileName = "ImageFile.jpg" } };
            MockMakerClient.Setup(x => x.ReadImageFile(It.Is<string>(p => p.Equals(project.LargeFileId)))).Returns(response);

            var projectCard = new ProjectCardModel(MockMakerClient.Object, project);
            Assert.AreEqual(response.File.FileName, projectCard.MasterFileName);
        }

        [TestMethod]
        public void ConstructorWillNotSetMasterFileImageIfFileReturnedEmpty()
        {
            var project = CreateProjectStructureHelper(true, true, true);
            MockMakerClient.Setup(x => x.ReadImageFile(It.Is<string>(p => p.Equals(project.LargeFileId)))).Returns(new ImageFileResponse() { });

            var projectCard = new ProjectCardModel(MockMakerClient.Object, project);

            Assert.IsTrue(String.IsNullOrEmpty(projectCard.MasterFileName));
        }

        [TestMethod]
        public void ConstructorWillNotSetMasterFileImageIfLargeFileIdEmpty()
        {
            var project = CreateProjectStructureHelper(true, false, false);
            var projectCard = new ProjectCardModel(MockMakerClient.Object, project);

            Assert.IsTrue(String.IsNullOrEmpty(projectCard.MasterFileName));
        }

        [TestMethod]
        public void ConstructorSetsTileImageCountTo0IfSmallFileIdsEmpty()
        {
            var project = CreateProjectStructureHelper(false, false, false);
            var projectCard = new ProjectCardModel(MockMakerClient.Object, project);

            Assert.AreEqual(0, projectCard.TileImageCount);
        }

        [TestMethod]
        public void ConstructorSetMosaicLocationToEmptyIfMosaicNotGenerated()
        {
            var project = CreateProjectStructureHelper(true, true, false);
            var projectCard = new ProjectCardModel(MockMakerClient.Object, project);

            Assert.IsTrue(String.IsNullOrEmpty(projectCard.MosaicLocation));
        }
    }
}
