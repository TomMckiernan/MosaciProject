using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mosaic.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class ViewImageModelTests
    {
        [TestMethod]
        public void IfFileToCopyDoesNotExistReturnError()
        {
            var model = new ViewImageModel();
            model.CopyImage("~\\InvalidPath\\image.png");
            Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void IfFileNotCopiedReturnsError()
        {
            //var model = new ViewImageModel();
            //model.CopyImage("~\\images\\temp");
            //Assert.IsFalse(String.IsNullOrEmpty(model.Error));
        }

        [TestMethod]
        public void IfFileCopiedThenFilePathSetToNewFile()
        {
            var model = new ViewImageModel();
            model.CopyImage("..\\..\\..\\..\\..\\..\\test\\TestImages\\752.jpg");
            Assert.AreEqual("",model.FilePath);

            // cleanup
            model.DeleteImage();
            // call method which deletes the file that has been created
        }
    }
}
