﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mosaic.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class ViewImageModelTests
    {
        public string copyLocation = "C:\\Users\\Tom_m\\OneDrive\\Documents\\MosaicProject\\Mosaic\\Mosaic.WebUI\\Mosaic.WebUI\\wwwroot\\images\\test\\";

        [TestMethod]
        public void IfCopyDirectoryDoesNotExistReturnsError()
        {
            var model = new ViewImageModel("~\\InvalidLocation\\");
            model.CopyImage("~\\InvalidPath\\image.png");
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void IfFileToCopyDoesNotExistReturnError()
        {
            var model = new ViewImageModel(copyLocation);
            model.CopyImage("~\\InvalidPath\\image.png");
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void IfFileCopiedThenFilePathSetToNewFile()
        {
            var filePath = "..\\..\\..\\..\\..\\..\\test\\TestImages\\752.jpg";
            var model = new ViewImageModel(copyLocation);
            model.CopyImage(filePath);
            Assert.AreEqual(copyLocation + Path.GetFileName(filePath), model.FilePath);

            Assert.IsTrue(File.Exists(model.FilePath));
            // cleanup
            model.DeleteImage(model.FilePath);
            Assert.IsFalse(File.Exists(model.FilePath));

            // call method which deletes the file that has been created
        }

        [TestMethod]
        public void IfFileNameSetCopiedImageWillBeRenamedToFileName()
        {
            var filePath = "..\\..\\..\\..\\..\\..\\test\\TestImages\\752.jpg";
            var model = new ViewImageModel(copyLocation);
            var newFileName = "renamedFile";
            model.CopyImage(filePath, newFileName);
            Assert.AreEqual(copyLocation + newFileName + Path.GetExtension(filePath), model.FilePath);

            Assert.AreEqual(newFileName + Path.GetExtension(filePath), Path.GetFileName(model.ImagePath));
            Assert.IsTrue(File.Exists(model.FilePath));
            // cleanup
            model.DeleteImage(model.FilePath);
            Assert.IsFalse(File.Exists(model.FilePath));
        }
    }
}
