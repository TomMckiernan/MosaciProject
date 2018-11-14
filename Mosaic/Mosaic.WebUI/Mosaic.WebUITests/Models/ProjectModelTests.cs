using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

        [TestClass]
        public class MyTestClass
        {

        }
    }
}
