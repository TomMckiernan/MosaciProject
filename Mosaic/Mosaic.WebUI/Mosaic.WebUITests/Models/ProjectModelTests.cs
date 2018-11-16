using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mosaic.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class ProjectModelTests
    {
        Mock<IMakerClient> MockMakerClient;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMakerClient = new Mock<IMakerClient>();
            MockMakerClient.Setup(x => x.CreateProject()).Returns(new ProjectResponse { });
            MockMakerClient.Setup(x => x.ReadAllProjects()).Returns(new ProjectMultipleResponse { });
        }

        [TestMethod]
        public void CreateProjectReturnsProjectResponse()
        {
            var response = new ProjectModel().CreateProject(MockMakerClient.Object);
            Assert.IsTrue(response.GetType().Equals(typeof(ProjectResponse)));
        }

        [TestMethod]
        public void ReadAllProjectsReturnsProjectMultipleResponse()
        {
            var response = new ProjectModel().ReadAllProjects(MockMakerClient.Object);
            Assert.IsTrue(response.GetType().Equals(typeof(ProjectMultipleResponse)));
        }
    }
}
