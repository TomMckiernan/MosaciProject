using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mosaic.WebUITests
{
    [TestClass]
    public class ErrorViewModelTests
    {
        [TestMethod]
        public void RequestIdNotNullShowRequestedIdReturnsTrue()
        {
            var errorViewModel = new ErrorViewModel();
            errorViewModel.RequestId = "requestId";
            Assert.IsTrue(errorViewModel.ShowRequestId);
        }
    }
}
