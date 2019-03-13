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
            var tile = new MosaicTile() { Difference = 5 };
            var mosaicTiles = new List<MosaicTile>() { tile };
            var result = mosaicTiles.GetAverage();
            Assert.AreEqual(tile.Difference, result);
        }

        [TestMethod]
        public void GetAverageReturnsAverageOfDifferenceForBothItemsInList()
        {
            var tile1 = new MosaicTile() { Difference = 2 };
            var tile2 = new MosaicTile() { Difference = 4 };
            var mosaicTiles = new List<MosaicTile>() { tile1, tile2 };
            var result = mosaicTiles.GetAverage();
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GetUniqueImagesReturnEmptyListIfListEmpty()
        {
            var mosaicTiles = new List<MosaicTile>();
            var result = mosaicTiles.GetUniqueImages();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetUniqueImagesReturnOneItemIfOneItemInList()
        {
            var tile = new MosaicTile() { Image = "test.png" };
            var mosaicTiles = new List<MosaicTile>() { tile };
            var result = mosaicTiles.GetUniqueImages();
            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.Contains(tile.Image));
        }

        [TestMethod]
        public void GetUniqueImagesReturnAllUniqueQuadrantImages()
        {
            var TLTile = new MosaicTile() { Image = "testTL.png" };
            var TRTile = new MosaicTile() { Image = "testTR.png" };
            var BLTile = new MosaicTile() { Image = "testBL.png" };
            var BRTile = new MosaicTile() { Image = "testBR.png" };
            var tile = new MosaicTile() { Image = "test.png", InQuadrants = true ,TLTile = TLTile, TRTile = TRTile, BLTile = BLTile, BRTile = BRTile };
            var mosaicTiles = new List<MosaicTile>() { tile };
            var result = mosaicTiles.GetUniqueImages();
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void GetUniqueImagesReturnOneImageIfQuadrantsSameAsWhole()
        {
            var TLTile = new MosaicTile() { Image = "test.png" };
            var TRTile = new MosaicTile() { Image = "test.png" };
            var BLTile = new MosaicTile() { Image = "test.png" };
            var BRTile = new MosaicTile() { Image = "test.png" };
            var tile = new MosaicTile() { Image = "test.png", InQuadrants = true, TLTile = TLTile, TRTile = TRTile, BLTile = BLTile, BRTile = BRTile };
            var mosaicTiles = new List<MosaicTile>() { tile };
            var result = mosaicTiles.GetUniqueImages();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void CalculateEdgeTilesMakesNoChangesIfPixelCoordinatesEmpty()
        {
            var tile = new MosaicTile() { Image = "test.png" };
            var mosaicTiles = new List<MosaicTile>() { tile };
            var coords = new List<PixelCoordinates>();
            var result = mosaicTiles.CalculateEdgeTiles(coords, 10, 10);
            Assert.AreEqual(mosaicTiles, result);
        }

        [TestMethod]
        public void CalculateEdgeTilesSetFirstTileAsEdgeIfPixelCoordsAtZero()
        {
            var tile = new MosaicTile() { Image = "test.png", X = 0, Y = 0 };
            var mosaicTiles = new List<MosaicTile>() { tile };
            var coords = new List<PixelCoordinates>() { new PixelCoordinates() { X = 0, Y = 0 } };
            var result = mosaicTiles.CalculateEdgeTiles(coords, 10, 10);
            Assert.IsTrue(result[0].IsEdge);
        }

        [TestMethod]
        public void CalculateEdgeTilesSetsMultipleTilesToEdgeCorrectly()
        {
            var tile1 = new MosaicTile() { Image = "test.png", X = 0, Y = 0 };
            var tile2 = new MosaicTile() { Image = "test.png", X = 1, Y = 1 };
            var mosaicTiles = new List<MosaicTile>() { tile1, tile2 };
            var coords = new List<PixelCoordinates>() { new PixelCoordinates() { X = 0, Y = 0 }, new PixelCoordinates() { X = 11, Y = 11} };
            var result = mosaicTiles.CalculateEdgeTiles(coords, 10, 10);
            Assert.IsTrue(tile1.IsEdge);
            Assert.IsTrue(tile2.IsEdge);

        }
    }
}
