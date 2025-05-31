using Microsoft.VisualStudio.TestTools.UnitTesting;
using BmsManager;
using System;

namespace BmsManager.Tests
{
    [TestClass]
    public class BmsManagerExceptionTests
    {
        [TestMethod]
        public void BmsManagerException_WithMessage_SetsMessage()
        {
            var message = "Test error message";
            var exception = new BmsManagerException(message);

            Assert.AreEqual(message, exception.Message);
        }

        [TestMethod]
        public void BmsManagerException_WithMessageAndInnerException_SetsBoth()
        {
            var message = "Test error message";
            var innerException = new InvalidOperationException("Inner exception");
            var exception = new BmsManagerException(message, innerException);

            Assert.AreEqual(message, exception.Message);
            Assert.AreEqual(innerException, exception.InnerException);
        }

        [TestMethod]
        public void BmsManagerException_InheritsFromException()
        {
            var exception = new BmsManagerException("Test");

            Assert.IsInstanceOfType(exception, typeof(Exception));
        }
    }
}
