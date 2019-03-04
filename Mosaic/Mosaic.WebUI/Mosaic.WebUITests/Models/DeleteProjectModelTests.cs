using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Moq;
using Mosaic.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class DeleteProjectModelTests
    {
        public string masterPath = "C:\\Users\\Tom_m\\OneDrive\\Documents\\MosaicProject\\Mosaic\\Mosaic.WebUI\\Mosaic.WebUITests\\images\\master\\";
        public string mosaicPath = "C:\\Users\\Tom_m\\OneDrive\\Documents\\MosaicProject\\Mosaic\\Mosaic.WebUI\\Mosaic.WebUITests\\images\\project\\";
        public string edgePath = "C:\\Users\\Tom_m\\OneDrive\\Documents\\MosaicProject\\Mosaic\\Mosaic.WebUI\\Mosaic.WebUITests\\images\\edges\\";

        Mock<IMakerClient> MockMakerClient;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMakerClient = new Mock<IMakerClient>();
        }

        [TestMethod]
        public void DeleteProjectReturnsErrorIfIdIsNull()
        {
            var model = new DeleteProjectModel();
            model.DeleteProject(MockMakerClient.Object, null);
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
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
            var project = new ProjectStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                TimeOfCreation = DateTime.Now.ToString(),
                Progress = ProjectStructure.Types.State.Largeadded,
                MasterLocation = "//InvalidLocation"
            };
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(new ProjectResponse { Project = project });
            MockMakerClient.Setup(x => x.DeleteProject(It.IsAny<string>())).Returns(new ProjectResponse { Error = "Error" });

            var model = new DeleteProjectModel();
            model.DeleteProject(MockMakerClient.Object, ObjectId.GenerateNewId().ToString());
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void DeleteMasterWillDeleteLocalMasterFileIfExists()
        {
            var masterLocation = masterPath + "Master.txt";
            // Closes the file after creating it
            File.Create(masterLocation).Dispose();

            var project = new ProjectStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                TimeOfCreation = DateTime.Now.ToString(),
                Progress = ProjectStructure.Types.State.Completed,
                MasterLocation = masterLocation
            };
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(new ProjectResponse { Project = project });
            MockMakerClient.Setup(x => x.DeleteProject(It.IsAny<string>())).Returns(new ProjectResponse { Error = "Error" });

            var model = new DeleteProjectModel(masterPath, mosaicPath);
            model.DeleteProject(MockMakerClient.Object, ObjectId.GenerateNewId().ToString());
            Assert.IsFalse(File.Exists(masterLocation));
        }

        [TestMethod]
        public void DeleteMosaicImageReturnErrorIfImageDoesNotExist()
        {
            var project = new ProjectStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                TimeOfCreation = DateTime.Now.ToString(),
                Progress = ProjectStructure.Types.State.Completed,
                MosaicLocation = "//InvalidLocation"
            };
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(new ProjectResponse { Project = project });
            MockMakerClient.Setup(x => x.DeleteProject(It.IsAny<string>())).Returns(new ProjectResponse { Error = "Error" });

            var model = new DeleteProjectModel();
            model.DeleteProject(MockMakerClient.Object, ObjectId.GenerateNewId().ToString());
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void DeleteMosaicWillDeleteLocalMosaicFileIfExists()
        {
            var mosaicLocation = mosaicPath + "Mosaic.txt";
            // Closes the file after creating it
            File.Create(mosaicLocation).Dispose();

            var project = new ProjectStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                TimeOfCreation = DateTime.Now.ToString(),
                Progress = ProjectStructure.Types.State.Completed,
                MosaicLocation = mosaicLocation
            };
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(new ProjectResponse { Project = project });
            MockMakerClient.Setup(x => x.DeleteProject(It.IsAny<string>())).Returns(new ProjectResponse { Error = "Error" });

            var model = new DeleteProjectModel(masterPath, mosaicPath);
            model.DeleteProject(MockMakerClient.Object, ObjectId.GenerateNewId().ToString());
            Assert.IsFalse(File.Exists(mosaicLocation));
        }

        [TestMethod]
        public void DeleteEdgeImageReturnErrorIfImageDoesNotExist()
        {
            var project = new ProjectStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                TimeOfCreation = DateTime.Now.ToString(),
                Progress = ProjectStructure.Types.State.Completed,
                EdgeLocation = "//InvalidLocation"
            };
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(new ProjectResponse { Project = project });
            MockMakerClient.Setup(x => x.DeleteProject(It.IsAny<string>())).Returns(new ProjectResponse { Error = "Error" });

            var model = new DeleteProjectModel();
            model.DeleteProject(MockMakerClient.Object, ObjectId.GenerateNewId().ToString());
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void DeleteEdgeWillDeleteLocalMosaicFileIfExists()
        {
            var edgeLocation = edgePath + "Edge.txt";
            // Closes the file after creating it
            File.Create(edgeLocation).Dispose();

            var project = new ProjectStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                TimeOfCreation = DateTime.Now.ToString(),
                Progress = ProjectStructure.Types.State.Completed,
                EdgeLocation = edgeLocation
            };
            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(new ProjectResponse { Project = project });
            MockMakerClient.Setup(x => x.DeleteProject(It.IsAny<string>())).Returns(new ProjectResponse { Error = "Error" });

            var model = new DeleteProjectModel(masterPath, mosaicPath, edgePath);
            model.DeleteProject(MockMakerClient.Object, ObjectId.GenerateNewId().ToString());
            Assert.IsFalse(File.Exists(edgeLocation));
        }

        [TestMethod]
        public void DeleteProjectWillReturnErrorIfProjectCannotBeDeleted()
        {
            var project = new ProjectStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                TimeOfCreation = DateTime.Now.ToString(),
                Progress = ProjectStructure.Types.State.Completed,
            };

            MockMakerClient.Setup(x => x.ReadProject(It.IsAny<string>())).Returns(new ProjectResponse { Project = project });
            MockMakerClient.Setup(x => x.DeleteProject(It.IsAny<string>())).Returns(new ProjectResponse { Error = "Error" });

            var model = new DeleteProjectModel();
            model.DeleteProject(MockMakerClient.Object, ObjectId.GenerateNewId().ToString());
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }
    }
}
