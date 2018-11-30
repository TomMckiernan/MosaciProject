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
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var Id = ObjectId.GenerateNewId().ToString();
            var model = new GenerateMosaicModel();
            var response = model.Generate(MockMakerClient.Object, Id);
            Assert.IsFalse(String.IsNullOrEmpty(response));
        }
    }
}
