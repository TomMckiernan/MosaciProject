using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Mosaic.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class GenerateMosaicModelTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var request = new ProjectStructure() { Id = ObjectId.GenerateNewId().ToString() };
            var model = new GenerateMosaicModel();
            var response = model.Generate(request);
            Assert.IsFalse(String.IsNullOrEmpty(response));
        }
    }
}
