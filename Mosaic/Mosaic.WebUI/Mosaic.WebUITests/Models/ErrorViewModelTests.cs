using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mosaic.WebUI.Models;

namespace Mosaic.WebUITests.Models
{
    [TestClass]
    public class ErrorViewModelTests
    {
        [TestMethod]
        public void RequestIdNotNullShowRequestIdTrue()
        {
            var errorViewModel = new ErrorViewModel();
            errorViewModel.RequestId = "requestId";
            Assert.IsTrue(errorViewModel.ShowRequestId);
        }

        [TestMethod]
        public void RequestIdNullShowRequestIdFalse()
        {
            var errorViewModel = new ErrorViewModel();
            Assert.IsFalse(errorViewModel.ShowRequestId);
        }
    }
}
