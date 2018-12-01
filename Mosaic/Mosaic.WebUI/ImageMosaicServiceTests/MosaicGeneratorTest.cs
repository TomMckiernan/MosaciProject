using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ImageMosaicService;
using System.IO;

namespace ImageMosaicTest
{
    [TestClass]
    public class MosaicGeneratorTest
    {
        ImageProcessing imageProcessing;
        string sourceFile = "..\\..\\..\\..\\..\\..\\test\\TestImages\\752.jpg";

        [TestInitialize]
        public void Init()
        {
            imageProcessing = new ImageProcessing();
        }

        [TestMethod]
        public void ResizeTest()
        {
            using (var resizedBmp = imageProcessing.Resize(sourceFile))
            {
                Assert.IsTrue(resizedBmp != null);
                Assert.IsTrue(resizedBmp.Height == 119);
                Assert.IsTrue(resizedBmp.Width == 119);
            }
        }

        [TestMethod]
        public void AverageColorTest()
        {
            using (var input = Bitmap.FromFile(sourceFile))
            {
                var inputBmp = new Bitmap(input);
                var imageInfo = imageProcessing.GetAverageColor(inputBmp, sourceFile);

                Assert.IsTrue(!string.IsNullOrEmpty(imageInfo.Path));
                Assert.IsTrue(!imageInfo.AverageBL.IsEmpty);
                Assert.IsTrue(!imageInfo.AverageBR.IsEmpty);
                Assert.IsTrue(!imageInfo.AverageTL.IsEmpty);
                Assert.IsTrue(!imageInfo.AverageTR.IsEmpty);
            }
        }

        [TestMethod]
        public void CreateMapTest()
        {
            using (var input = Bitmap.FromFile(sourceFile))
            {
                var inputBmp = new Bitmap(input);
                var _createMap = imageProcessing.CreateMap(inputBmp);

                Assert.IsTrue(_createMap.Length > 0);
            }
        }

        //This seemed to crash when no jpg were found in the image folder
        // Also generic error when saving to c:temp 
        [TestMethod]
        public void MosaicGenerator_Test_END_TO_END()
        {
            var mosaicGenerator = new MosaicGenerator();
            var srcImage = "..\\..\\..\\..\\..\\..\\test\\TestImages\\752.jpg";
            var imageFolder = "C:\\Users\\Tom_m\\OneDrive\\Pictures\\Test7Images";

            var tileImages = new List<string>();
            foreach (var path in Directory.GetFiles(imageFolder))
            {
                tileImages.Add(Path.GetFullPath(path));
            }

            var mosaic = mosaicGenerator.Generate(srcImage,tileImages);

            mosaic.Image.Save(string.Format("..\\..\\..\\..\\..\\..\\test\\{0}.jpg", Guid.NewGuid().ToString("N")));
            mosaic.Image.Dispose();
        }

        //This seemed to crash when no jpg were found in the image folder
        // Also generic error when saving to c:temp 
        [TestMethod]
        public void MosaicGenerator_BlockColourTest()
        {
            var mosaicGenerator = new MosaicGenerator();
            var srcImage = "..\\..\\..\\..\\..\\..\\test\\BlockColourTests\\rgb-circles.jpg";
            var imageFolder = "..\\..\\..\\..\\..\\..\\test\\BlockColours";

            var tileImages = new List<string>();
            foreach (var path in Directory.GetFiles(imageFolder))
            {
                tileImages.Add(Path.GetFullPath(path));
            }

            var mosaic = mosaicGenerator.Generate(srcImage, tileImages);

            mosaic.Image.Save(string.Format("..\\..\\..\\..\\..\\..\\test\\{0}.jpg", Guid.NewGuid().ToString("N")));
            mosaic.Image.Dispose();
        }

        //This seemed to crash when no jpg were found in the image folder
        // Also generic error when saving to c:temp 
        [TestMethod]
        public void MosaicGenerator_DogTest()
        {
            var mosaicGenerator = new MosaicGenerator();
            var srcImage = "..\\..\\..\\..\\..\\..\\test\\SourceImages\\DogImage1.png";
            var imageFolder = "C:\\Users\\Tom_m\\OneDrive\\Pictures\\clubs";

            var tileImages = new List<string>();
            foreach (var path in Directory.GetFiles(imageFolder))
            {
                tileImages.Add(Path.GetFullPath(path));
            }

            var mosaic = mosaicGenerator.Generate(srcImage, tileImages);

            mosaic.Image.Save(string.Format("..\\..\\..\\..\\..\\..\\test\\{0}.jpg", Guid.NewGuid().ToString("N")));
            mosaic.Image.Dispose();
        }

        //This seemed to crash when no jpg were found in the image folder
        // Also generic error when saving to c:temp 
        [TestMethod]
        public void MosaicGenerator_DogTestSmall()
        {
            var mosaicGenerator = new MosaicGenerator();
            var srcImage = "..\\..\\..\\..\\..\\..\\test\\SourceImages\\DogImage1Small2.png";
            var imageFolder = "C:\\Users\\Tom_m\\OneDrive\\Pictures\\clubs";

            var tileImages = new List<string>();
            foreach (var path in Directory.GetFiles(imageFolder))
            {
                tileImages.Add(Path.GetFullPath(path));
            }

            var mosaic = mosaicGenerator.Generate(srcImage, tileImages);

            mosaic.Image.Save(string.Format("..\\..\\..\\..\\..\\..\\test\\{0}.jpg", Guid.NewGuid().ToString("N")));
            mosaic.Image.Dispose();
        }

        //This seemed to crash when no jpg were found in the image folder
        // Also generic error when saving to c:temp 
        [TestMethod]
        public void MosaicGenerator_DogTest_2()
        {
            var mosaicGenerator = new MosaicGenerator();
            var srcImage = "..\\..\\..\\..\\..\\..\\test\\SourceImages\\DogImage2.png";
            var imageFolder = "C:\\Users\\Tom_m\\OneDrive\\Pictures\\clubs";

            var tileImages = new List<string>();
            foreach (var path in Directory.GetFiles(imageFolder))
            {
                tileImages.Add(Path.GetFullPath(path));
            }

            var mosaic = mosaicGenerator.Generate(srcImage, tileImages);

            mosaic.Image.Save(string.Format("..\\..\\..\\..\\..\\..\\test\\{0}.jpg", Guid.NewGuid().ToString("N")));
            mosaic.Image.Dispose();
        }

        //This seemed to crash when no jpg were found in the image folder
        // Also generic error when saving to c:temp 
        [TestMethod]
        public void MosaicGenerator_DogTestSmall_2()
        {
            var mosaicGenerator = new MosaicGenerator();
            var srcImage = "..\\..\\..\\..\\..\\..\\test\\SourceImages\\DogImage2Small2.png";
            var imageFolder = "C:\\Users\\Tom_m\\OneDrive\\Pictures\\clubs";

            var tileImages = new List<string>();
            foreach (var path in Directory.GetFiles(imageFolder))
            {
                tileImages.Add(Path.GetFullPath(path));
            }

            var mosaic = mosaicGenerator.Generate(srcImage, tileImages);

            mosaic.Image.Save(string.Format("..\\..\\..\\..\\..\\..\\test\\{0}.jpg", Guid.NewGuid().ToString("N")));
            mosaic.Image.Dispose();
        }

        //This seemed to crash when no jpg were found in the image folder
        // Also generic error when saving to c:temp 
        [TestMethod]
        public void MosaicGenerator_PngTest()
        {
            var mosaicGenerator = new MosaicGenerator();
            var srcImage = "..\\..\\..\\..\\..\\..\\test\\SourceImages\\144196672.png";
            var imageFolder = "C:\\Users\\Tom_m\\OneDrive\\Pictures\\clubs";

            var tileImages = new List<string>();
            foreach (var path in Directory.GetFiles(imageFolder))
            {
                tileImages.Add(Path.GetFullPath(path));
            }

            var mosaic = mosaicGenerator.Generate(srcImage, tileImages);

            mosaic.Image.Save(string.Format("..\\..\\..\\..\\..\\..\\test\\{0}.jpg", Guid.NewGuid().ToString("N")));
            mosaic.Image.Dispose();
        }
    }
}

