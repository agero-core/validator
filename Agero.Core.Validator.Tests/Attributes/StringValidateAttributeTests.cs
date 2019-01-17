using Agero.Core.Validator.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Agero.Core.Validator.Tests.Attributes
{
    [TestClass]
    public class StringValidateAttributeTests
    {
        [TestMethod]
        public void Constructor_Should_Set_Default_Values()
        {
            // Act
            var attr = new StringValidateAttribute("Test message");

            // Assert
            Assert.AreEqual(-1, attr.MinLength);
            Assert.AreEqual(-1, attr.MaxLength);
            Assert.IsTrue(attr.CanBeEmpty);
            Assert.IsTrue(attr.CanBeNull);
            Assert.IsNull(attr.RegEx);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void IsValid_Should_Throw_Exception_When_Value_Is_Not_String()
        {
            // Arrange
            var attr = new StringValidateAttribute("Test message");

            // Act
            // Assert
            attr.IsValid(5);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_When_Value_Is_Less_Than_MinLength()
        {
            // Arrange
            var attr = new StringValidateAttribute("Test message") { MinLength = 3 };

            // Act
            bool result = attr.IsValid("12");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_When_Value_Is_Greater_Than_MaxLength()
        {
            // Arrange
            var attr = new StringValidateAttribute("Test message") { MaxLength = 3 };

            // Act
            bool result = attr.IsValid("1234");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_When_Value_Is_Null_And_CanBeNull_Is_False()
        {
            // Arrange
            var attr = new StringValidateAttribute("Test message") { CanBeNull = false };

            // Act
            bool result = attr.IsValid(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_When_Value_Is_Empty_And_CanBeEmpty_Is_False()
        {
            // Arrange
            var attr = new StringValidateAttribute("Test message") { CanBeEmpty = false };

            // Act
            bool result = attr.IsValid(string.Empty);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_When_Value_Is_Not_Match_Regex()
        {
            // Arrange
            var attr = new StringValidateAttribute("Test message") { RegEx = @"^[0-1]$" };

            // Act
            bool result = attr.IsValid("2");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_True_When_Parameters_Not_Specified()
        {
            var values =
                new[]
                {
                    "",
                    null,
                    "1234567890",
                    "abcdefghjk",
                    "~!@#$%^&*()_+"
                };

            foreach (var value in values)
            {
                // Arrange
                var attr = new StringValidateAttribute("Test message");

                // Act
                bool result = attr.IsValid(value);

                // Assert
                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void IsValid_Should_Return_True_When_Value_Meet_All_Requirements()
        {
            // Arrange
            var attr = new StringValidateAttribute("Test message")
                           {
                               MinLength = 1,
                               MaxLength = 3,
                               CanBeNull = false,
                               CanBeEmpty = false,
                               RegEx = @"^[0]$"
                           };

            // Act
            bool result = attr.IsValid("0");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_True_When_Value_Is_Empty_And_Not_Meet_All_Requirements_Except_CanBeEmpty()
        {
            // Arrange
            var attr = new StringValidateAttribute("Test message")
            {
                MinLength = 1,
                MaxLength = 3,
                CanBeNull = false,
                CanBeEmpty = true,
                RegEx = @"^[0]$"
            };

            // Act
            bool result = attr.IsValid(string.Empty);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_Should_Return_True_When_Value_Is_Null_And_Not_Meet_All_Requirements_Except_CanBeNull()
        {
            // Arrange
            var attr = new StringValidateAttribute("Test message")
            {
                MinLength = 1,
                MaxLength = 3,
                CanBeNull = true,
                CanBeEmpty = false,
                RegEx = @"^[0]$"
            };

            // Act
            bool result = attr.IsValid(null);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
