using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mosaic.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Utility;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class GenerateMosaicColoursModelTests
    {
        Mock<IMakerClient> MockMakerClient;

        [TestInitialize]
        public void SetUpRepository()
        {
            MockMakerClient = new Mock<IMakerClient>();
            MockMakerClient.Setup(x => x.ReadAllImageFiles(It.IsAny<List<string>>())).Returns(new ImageFileIndexResponse { });
            MockMakerClient.Setup(x => x.ReadImageFile(It.IsAny<string>())).Returns(new ImageFileResponse { File = new ImageFileIndexStructure() { Id = "0" } });
        }

        [TestMethod]
        public void CalculateLibrarySuitablityWithEmptyLibrariesReturnsZero()
        {
            var model = new GenerateMosaicColoursModel();
            model.TileImageColorDictionary = new Dictionary<string, int>();
            model.MasterImageColorDictionary = new Dictionary<string, int>();
            var suitability = model.CalulateLibrarySuitability();
            Assert.AreEqual(0, suitability);
        }

        [TestMethod]
        public void CalculateLibrarySuitabilityWithEmptyMasterReturnsZero()
        {
            var model = new GenerateMosaicColoursModel();
            model.TileImageColorDictionary = new Dictionary<string, int>(){};
            model.TileImageColorDictionary.Add(Color.White.ToHex(), 1);
            model.MasterImageColorDictionary = new Dictionary<string, int>();
            var suitability = model.CalulateLibrarySuitability();
            Assert.AreEqual(0, suitability);
        }

        [TestMethod]
        public void CalculateLibrarySuitabilityWithEmptyTilesReturnsZero()
        {
            var model = new GenerateMosaicColoursModel();
            model.TileImageColorDictionary = new Dictionary<string, int>();
            model.MasterImageColorDictionary = new Dictionary<string, int>();
            model.MasterImageColorDictionary.Add(Color.White.ToHex(), 1);
            var suitability = model.CalulateLibrarySuitability();
            Assert.AreEqual(0, suitability);
        }

        [TestMethod]
        public void CalculateLibrarySuitabilityOfCompleteMatchReturns100()
        {
            var model = new GenerateMosaicColoursModel();
            model.TileImageColorDictionary = new Dictionary<string, int>();
            model.TileImageColorDictionary.Add(Color.White.ToHex(), 1);
            model.MasterImageColorDictionary = new Dictionary<string, int>();
            model.MasterImageColorDictionary.Add(Color.White.ToHex(), 1);
            var suitability = model.CalulateLibrarySuitability();
            Assert.AreEqual(100, suitability);
        }
    }
}
