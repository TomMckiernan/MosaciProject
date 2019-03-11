using ImageMosaicService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageMosaicServiceTests
{
    [TestClass]
    public class MosaicTileTests
    {
        [TestMethod]
        public void ConstructorContructsObjectCorrectly()
        {
            var x = 1;
            var y = 1;
            var filePath = "filePath";
            var difference = 5;
            var imageInfo = new ImageInfo(filePath);
            imageInfo.Difference = difference;
            var result = new MosaicTile(imageInfo, x, y);
            Assert.AreEqual(result.X, x);
            Assert.AreEqual(result.Y, y);
            Assert.AreEqual(result.Image, filePath);
            Assert.AreEqual(result.Difference, difference);
        }

        [TestMethod]
        public void GetAverageReturnsZeroIfListEmpty()
        {
            var mosaicTiles = new List<MosaicTile>();
            var result = mosaicTiles.GetAverage();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAverageReturnsDifferenceIfOneItemInList()
        {
            var tile = new MosaicTile();
            tile.Difference = 5;
            var mosaicTiles = new List<MosaicTile>() { tile };
            var result = mosaicTiles.GetAverage();
            Assert.AreEqual(tile.Difference, result);
        }

        [TestMethod]
        public void GetAverageReturnsAverageOfDifferenceForBothItemsInList()
        {
            var tile1 = new MosaicTile();
            tile1.Difference = 2;
            var tile2 = new MosaicTile();
            tile2.Difference = 4;
            var mosaicTiles = new List<MosaicTile>() { tile1, tile2 };
            var result = mosaicTiles.GetAverage();
            Assert.AreEqual(3, result);
        }
    }
}
