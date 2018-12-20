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
    public class DeleteProjectModelTests
    {
        Mock<IMakerClient> MockMakerClient;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMakerClient = new Mock<IMakerClient>();
        }

        [TestMethod]
        public void DeleteProjectReturnsErrorIfReadProjectReturnsError()
        {
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(new ProjectResponse { Error = "Error" });

            var model = new DeleteProjectModel();
            model.DeleteProject(MockMakerClient.Object, ObjectId.GenerateNewId().ToString());
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void DeleteMasterImageReturnErrorIfImageDoesNotExist()
        {
            var project = new ProjectResponse(){ Project = new ProjectStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                TimeOfCreation = DateTime.Now.ToString(),
                Progress = ProjectStructure.Types.State.Largeadded,
                MasterLocation = "InvalidLocation"
            }};
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(new ProjectResponse { Error = "Error" });

            var model = new DeleteProjectModel();
            model.DeleteProject(MockMakerClient.Object, ObjectId.GenerateNewId().ToString());
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void DeleteMasterWillDeleteLocalMasterFileIfExists()
        {
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(new ProjectResponse { Error = "Error" });

            var model = new DeleteProjectModel();
            model.DeleteProject(MockMakerClient.Object, ObjectId.GenerateNewId().ToString());
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void DeleteMosaicImageReturnErrorIfImageDoesNotExist()
        {

        }

        [TestMethod]
        public void DeleteMosaicWillDeleteLocalMosaicFileIfExists()
        {

        }

        [TestMethod]
        public void DeleteProjectWillReturnErrorIfProjectCannotBeDeleted()
        {

        }
    }
}
