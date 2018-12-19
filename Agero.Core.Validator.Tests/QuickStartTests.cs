using Agero.Core.Validator.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Agero.Core.Validator.Tests
{
    public class TestClass
    {
        [ByteValidate("ByteValue must have value between 1 and 100.", MinValue = 1, MaxValue = 100)]
        public byte ByteValue { get; set; }

        [DecimalValidate("DecimalValue must have value between 2.45 and 100.98", MinValueString = "2.45", MaxValueString = "100.98")]
        public decimal DecimalValue { get; set; }

        [DoubleValidate("DoubleValue must have value between 2.45 and 100.98", MinValue = 2.45, MaxValue = 100.98)]
        public double DoubleValue { get; set; }

        [IntValidate("IntValue must value between 1 and 1000", MinValue = 1, MaxValue = 1000)]
        public int IntValue { get; set; }

        [LongValidate("LongValue must value between 1 and 1000", MinValue = 1, MaxValue = 1000)]
        public long LongValue { get; set; }

        [NullableDecimalValidate("NullableDecimalValue must have value between 2.45 and 100.98", MinValueString = "2.45", MaxValueString = "100.98")]
        public decimal? NullableDecimalValue { get; set; }

        [NullableDoubleValidate("NullableDoubleValue must have value between 2.45 and 100.98", MinValue = 2.45, MaxValue = 100.98)]
        public double? NullableDoubleValue { get; set; }

        [NullableIntValidate("NullableIntValue must value between 1 and 1000", MinValue = 1, MaxValue = 1000)]
        public int? NullableIntValue { get; set; }

        [NullableLongValidate("NullableLongValue must value between 1 and 1000", MinValue = 1, MaxValue = 1000)]
        public long? NullableLongValue { get; set; }

        [NullableShortValidate("NullableShortValue must value between 1 and 1000", MinValue = 1, MaxValue = 1000)]
        public short? NullableShortValue { get; set; }

        [ShortValidate("ShortValue must value between 1 and 1000", MinValue = 1, MaxValue = 1000)]
        public short ShortValue { get; set; }

        [StringValidate("StringValue must have length between 2 and 100 characters.", MinLength = 2, MaxLength = 100, CanBeNull = true, CanBeEmpty = false)]
        public string StringValue { get; set; }

        [ComplexTypeValidate] // This attribute tells ValidationHelper to validate TestValueClass object
        [NotNullValidate("TestValue must be specified.")]
        public TestValueClass TestValue { get; set; }
    }

    public class TestValueClass
    {
        [NotNullValidate("Value must be specified.")]
        public object Value { get; set; }
    }

    [TestClass]
    public class QuickStartTests
    {
        #region Validate method Usage
        [TestMethod]
        public void Test_Validate_Should_Return_No_Errors()
        {
            //Arrange
            var testClass =
                new TestClass
                {
                    ByteValue = 5, 
                    DecimalValue = 3.5m,
                    DoubleValue = 3.5,
                    IntValue = 2, 
                    LongValue = 1000, 
                    NullableDecimalValue = null, 
                    NullableDoubleValue = null, 
                    NullableIntValue = null, 
                    NullableLongValue = null,
                    NullableShortValue = null,
                    ShortValue = 1000,
                    StringValue = "Some text",
                    TestValue = 
                        new TestValueClass
                        {
                            Value = "Some text"
                        }
                };

            //Act
            var validator = new ValidationHelper();
            var validationErrors = validator.Validate(testClass);

            //Assert
            Assert.IsNull(validationErrors);
        }

        [TestMethod]
        public void Test_Validate_Should_Return_Errors()
        {
            var myClass =
                new TestClass
                {
                    ByteValue = 5,
                    DecimalValue = 3.5m,
                    DoubleValue = 3.5,
                    IntValue = -1, //invalid
                    LongValue = 1000,
                    NullableDecimalValue = null,
                    NullableDoubleValue = null,
                    NullableIntValue = null,
                    NullableLongValue = null,
                    NullableShortValue = null,
                    ShortValue = 1000,
                    StringValue = "Some text",
                    TestValue =
                        new TestValueClass
                        {
                            Value = null //invalid
                        }
                };

            var validator = new ValidationHelper();
            var validationErrors = validator.Validate(myClass);

            Assert.AreSame(myClass, validationErrors.Object);
            Assert.AreEqual(2, validationErrors.Errors.Count);

            var error1 = validationErrors.Errors.First();
            Assert.AreEqual("IntValue", error1.Key);
            Assert.AreEqual("IntValue must value between 1 and 1000", error1.Message);

            var error2 = validationErrors.Errors.Last();
            Assert.AreEqual("Value", error2.Key);
            Assert.AreEqual("Value must be specified.", error2.Message);
        }
        #endregion

        #region IsValid method usage
        [TestMethod]
        public void Test_IsValid_Should_Return_True()
        {
            //Arrange
            var testClass =
                new TestClass
                {
                    ByteValue = 5,
                    DecimalValue = 3.5m,
                    DoubleValue = 3.5,
                    IntValue = 2,
                    LongValue = 1000,
                    NullableDecimalValue = null,
                    NullableDoubleValue = null,
                    NullableIntValue = null,
                    NullableLongValue = null,
                    NullableShortValue = null,
                    ShortValue = 1000,
                    StringValue = "Some text",
                    TestValue =
                        new TestValueClass
                        {
                            Value = "Some text"
                        }
                };

            //Act
            var validator = new ValidationHelper();
            var validationErrors = validator.IsValid(testClass);

            //Assert
            Assert.IsTrue(validationErrors);
        }

        [TestMethod]
        public void Test_IsValid_Should_Return_False()
        {
            //Arrange
            var testClass =
                new TestClass
                {
                    ByteValue = 5,
                    DecimalValue = 3.5m,
                    DoubleValue = 3.5,
                    IntValue = 2,
                    LongValue = 1000,
                    NullableDecimalValue = null,
                    NullableDoubleValue = null,
                    NullableIntValue = null,
                    NullableLongValue = null,
                    NullableShortValue = null,
                    ShortValue = 1000,
                    StringValue = "S", //invalid
                    TestValue =
                        new TestValueClass
                        {
                            Value = null //invalid
                        }
                };

            //Act
            var validator = new ValidationHelper();
            var validationErrors = validator.IsValid(testClass);

            //Assert
            Assert.IsFalse(validationErrors);
        }
        #endregion

        #region CheckIsValid method Usage
        [TestMethod]
        [ExpectedException(typeof(ValidationException<Agero.Core.Validator.Tests.TestClass>))]
        public void Test_CheckIsValid_Should_Throw_Exception()
        {
            //Arrange
            var testClass =
                new TestClass
                {
                    ByteValue = 5,
                    DecimalValue = 3.5m,
                    DoubleValue = 3.5,
                    IntValue = 2,
                    LongValue = 1000,
                    NullableDecimalValue = null,
                    NullableDoubleValue = null,
                    NullableIntValue = null,
                    NullableLongValue = null,
                    NullableShortValue = null,
                    ShortValue = 1000,
                    StringValue = "S", //invalid
                    TestValue =
                        new TestValueClass
                        {
                            Value = null //invalid
                        }
                };

            //Act
            var validator = new ValidationHelper();
            validator.CheckIsValid(testClass);
        }
        #endregion
    }
}