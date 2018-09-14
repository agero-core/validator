using Agero.Core.Validator.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agero.Core.Validator.Tests.Attributes
{
    [TestClass]
    public class EnumValidateAttributeTests
    {
        #region Test Classes

        private enum TestEnum
        {
            TestValue1,
            TestValue2,
        }

        #endregion


        #region Tests

        [TestMethod]
        public void IsValid_Should_Return_True_When_Value_Is_Valid_And_CanBeNull_Is_False()
        {
            // Arrange
            var attr = new EnumValidateAttribute("Enum error 1.", typeof (TestEnum)) {CanBeNull = false};

            // Act
            var result = attr.IsValid("TestValue2");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_When_Value_Is_Null_And_CanBeNull_Is_False()
        {
            // Arrange
            var attr = new EnumValidateAttribute("Enum error 1.", typeof(TestEnum)) { CanBeNull = false };

            // Act
            var result = attr.IsValid(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_When_Value_Is_Invalid_And_CanBeNull_Is_False()
        {
            // Arrange
            var attr = new EnumValidateAttribute("Enum error 1.", typeof(TestEnum)) { CanBeNull = false };

            // Act
            var result = attr.IsValid("ABC");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_True_When_Value_Is_Valid_And_CanBeNull_Is_True()
        {
            // Arrange
            var attr = new EnumValidateAttribute("Enum error 1.", typeof(TestEnum)) { CanBeNull = true };

            // Act
            var result = attr.IsValid("TestValue2");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_True_When_Value_Is_Valid_And_IgnoreCase_Is_True()
        {
            // Arrange
            var attr = new EnumValidateAttribute("Enum error 1.", typeof(TestEnum)) { CanBeNull = true , IgnoreCase = true };

            // Act
            var result = attr.IsValid("TESTValue2");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_When_Value_Has_Different_Case_And_IgnoreCase_Is_False()
        {
            // Arrange
            var attr = new EnumValidateAttribute("Enum error 1.", typeof(TestEnum)) { CanBeNull = true, IgnoreCase = false };

            // Act
            var result = attr.IsValid("TESTValue2");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_True_When_Value_Is_Null_And_CanBeNull_Is_True()
        {
            // Arrange
            var attr = new EnumValidateAttribute("Enum error 1.", typeof(TestEnum)) { CanBeNull = true };

            // Act
            var result = attr.IsValid(null);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_Flase_When_Value_Is_Invalid_And_CanBeNull_Is_True()
        {
            // Arrange
            var attr = new EnumValidateAttribute("Enum error 1.", typeof(TestEnum)) { CanBeNull = true };

            // Act
            var result = attr.IsValid("ABC");

            // Assert
            Assert.IsFalse(result);
        }

        #endregion
    }
}
