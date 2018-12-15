using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Mosaic.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Utility;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class TileTableModelTests
    {
        [TestMethod]
        public void ConvertEmptyImageFileIndexStructureReturnsEmptyModel()
        {
            var imageFile = new ImageFileIndexStructure();
            var model = new TileTableModel(imageFile);
            Assert.IsTrue(String.IsNullOrEmpty(model.Id));
            Assert.IsTrue(String.IsNullOrEmpty(model.FileName));
            Assert.IsTrue(String.IsNullOrEmpty(model.FilePath));
            Assert.IsTrue(String.IsNullOrEmpty(model.AverageColour));
            Assert.IsTrue(String.IsNullOrEmpty(model.LastWriteTime));
        }

        [TestMethod]
        public void ConvertImageFileIndexStructureConvertAverageWholeToHex()
        {
            var imageFile = new ImageFileIndexStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FileName = "FileName",
                FilePath = "FilePath\\FileName.png",
                LastWriteTime = DateTime.Now.ToString(),
                Data = new Metadata()
                {
                    AverageWhole = Color.Red.ToArgb()
                }
            };
            var model = new TileTableModel(imageFile);
            Assert.AreEqual(Color.Red.ToHex(), model.AverageColour);

        }

        [TestMethod]
        public void ConvertImageFileIndexStructureConvertEmptyAverageWholeToBlackHex()
        {
            var imageFile = new ImageFileIndexStructure()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                FileName = "FileName",
                FilePath = "FilePath\\FileName.png",
                LastWriteTime = DateTime.Now.ToString(),
                Data = new Metadata(){}
            };
            var model = new TileTableModel(imageFile);
            Assert.AreEqual(Color.Black.ToHex(), model.AverageColour);

        }
    }
}
