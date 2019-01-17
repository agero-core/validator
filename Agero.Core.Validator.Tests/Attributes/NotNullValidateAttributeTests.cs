using Agero.Core.Validator.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agero.Core.Validator.Tests.Attributes
{
    [TestClass]
    public class NotNullValidateAttributeTests
    {
        [TestMethod]
        public void IsValid_Should_Return_True_When_Value_Is_Not_Null()
        {
            // Arrange
            var attr = new NotNullValidateAttribute("Test message");

            // Act
            bool result = attr.IsValid("Value");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_When_Value_Is_Null()
        {
            // Arrange
            var attr = new NotNullValidateAttribute("Test message");

            // Act
            bool result = attr.IsValid(null);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
