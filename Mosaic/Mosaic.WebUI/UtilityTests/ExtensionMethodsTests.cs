using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using System.Drawing;
using Utility;

namespace UtilityTests
{
    [TestClass]
    public class ExtensionMethodsTests
    {
        [TestMethod]
        public void BsonDocumentInCorrectFormatReturnsCompleteIndexedLocationStructure()
        {
            var bson = new BsonDocument();
            var id = new BsonElement("_id", "0");
            var indexedLocation = new BsonElement("IndexedLocation", "IndexedLocation");
            bson.Add(id);
            bson.Add(indexedLocation);
            var result = new IndexedLocationStructure().ConvertFromBsonDocument(bson);
            Assert.AreEqual("0", result.Id);
            Assert.AreEqual("IndexedLocation", result.IndexedLocation);
        }

        [TestMethod]
        public void ColorToHexReturnsBlackHexValueIfColorEmpty()
        {
            var color = new Color();
            var hexColor = color.ToHex();
            Assert.AreEqual("#000000", hexColor);
        }

        [TestMethod]
        public void ColorToHexReturnsCorrectHexValueOfColorRepresented()
        {
            var color = Color.White;
            var hexColor = color.ToHex();
            Assert.AreEqual("#FFFFFF", hexColor);
        }

        [TestMethod]
        public void ColorArrayToListReturnsEmptyListIfColorArrayEmpty()
        {
            Color[,] colorArray = new Color[0,0];
            var colorList = colorArray.ToList();
            Assert.AreEqual(0, colorList.Count);
        }

        [TestMethod]
        public void ColorArrayToListReturnsOneColorIfArrayContainsOneColor()
        {
            Color[,] colorArray = new Color[1, 1];
            colorArray[0, 0] = Color.White;
            var colorList = colorArray.ToList();
            Assert.AreEqual(1, colorList.Count);
            Assert.IsTrue(colorList.Contains(Color.White));
        }

        [TestMethod]
        public void ColorArrayToListReturnsAllColorsStoredInArray()
        {
            Color[,] colorArray = new Color[2, 2];
            colorArray[0, 0] = Color.White;
            colorArray[1, 0] = Color.Red;
            colorArray[0, 1] = Color.Green;
            colorArray[1, 1] = Color.Blue;
            var colorList = colorArray.ToList();
            Assert.AreEqual(4, colorList.Count);
        }

        [TestMethod]
        public void ColorDifferenceBetweenSameColorIsZero()
        {
            var color = Color.White;
            var otherColor = Color.White;
            var difference = color.GetDifference(otherColor);
            Assert.AreEqual(0, difference);
        }

        [TestMethod]
        public void ColorDifferenceBetweenBalckAndWhiteIsOne()
        {
            var color = Color.White;
            var otherColor = Color.Black;
            var difference = color.GetDifference(otherColor);
            Assert.AreEqual(1, difference);
        }

        [TestMethod]
        public void BsonValueInCorrectFormatConvertsToPixelFormat()
        {
            var coord = new PixelCoordinates() { X = 1, Y = 2 };
            var bson = coord.ToBsonDocument().AsBsonValue;
            var result = bson.toPixelCoordinate();
            Assert.AreEqual(1, result.X);
            Assert.AreEqual(2, result.Y);
        }
    }
}
