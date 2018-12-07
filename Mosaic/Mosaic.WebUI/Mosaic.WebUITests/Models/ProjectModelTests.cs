using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Moq;
using Mosaic.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var response = new ProjectMultipleResponse() { };
            var expectedProjects = new List<ProjectStructure>()
            {
                new ProjectStructure { Id = ObjectId.GenerateNewId().ToString() },
                new ProjectStructure { Id = ObjectId.GenerateNewId().ToString() }
            };

            var returnObject = new ProjectMultipleResponse();
            returnObject.Projects.AddRange(expectedProjects);
            MockMakerClient.Setup(x => x.ReadAllProjects()).Returns(returnObject);

            var model = new ProjectModel();
            model.ReadAllProjects(MockMakerClient.Object);
            Assert.IsTrue(string.IsNullOrEmpty(model.Error));
            Assert.AreEqual(expectedProjects.Count, model.Projects.Count);
        }
    }
}
