using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mosaic.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class FileColourModelTests
    {
        [TestMethod]
        public void ColorRedReturnsColorRedAsClosestColour()
        {
            var model = new FileColourModel();
            var colours = new List<Color>() { Color.Red };
            var actual = model.FindClosestColour(colours);

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(Color.Red));
        }

        [TestMethod]
        public void EmptyListOfColoursReturnsEmptyListOfClosestColours()
        {
            var model = new FileColourModel();
            var colours = new List<Color>();
            var actual = model.FindClosestColour(colours);

            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void CloseColourWhiteReturnsWhiteAsClosestColour()
        {
            var model = new FileColourModel();
            var closeWhite = Color.FromArgb(255, 255, 254);
            var colours = new List<Color>() { closeWhite };
            var actual = model.FindClosestColour(colours);

            Assert.AreEqual(1, actual.Count);
            Assert.IsTrue(actual.Contains(Color.White));
        }

        [TestMethod]
        public void TwoCloseColoursReturnTwoClosestPreDfinedColours()
        {
            var model = new FileColourModel();
            var closeWhite = Color.FromArgb(255, 255, 254);
            var closeBlue = Color.FromArgb(0, 0, 254);
            var colours = new List<Color>() { closeWhite, closeBlue };
            var actual = model.FindClosestColour(colours);

            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.Contains(Color.White));
            Assert.IsTrue(actual.Contains(Color.Blue));
        }
    }
}
