using System;
using System.Globalization;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>Validation attribute which specifies that property of <see cref="decimal"/> type needs to be validated by <see cref="ValidationHelper"/>.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DecimalValidateAttribute : ValidateAttribute
    {
        private static readonly NumberFormatInfo _numberFormatInfo = new NumberFormatInfo { CurrencyDecimalSeparator = ".", NumberDecimalSeparator = "." };

        /// <summary>Constructor</summary>
        /// <param name="message">Validation message which is used in validation error.</param>
        public DecimalValidateAttribute(string message)
            : base(message)
        {
            MinValueString = decimal.MinValue.ToString(_numberFormatInfo);
            MaxValueString = decimal.MaxValue.ToString(_numberFormatInfo);
        }

        /// <summary>Specifies optional minimum value. This readonly property is calculated from <see cref="MinValueString"/> value.</summary>
        public decimal MinValue => decimal.Parse(MinValueString, _numberFormatInfo);

        /// <summary>Specifies optional maximum value. This readonly property is calculated from <see cref="MaxValueString"/> value.</summary>
        public decimal MaxValue => decimal.Parse(MaxValueString, _numberFormatInfo);

        /// <summary>Specifies optional minimum value as string, because decimal type do not have compile time literals.</summary>
        public string MinValueString { get; set; }
        
        //// <summary>Specifies optional maximum value as string, because decimal type do not have compile time literals.</summary>
        public string MaxValueString { get; set; }

        /// <summary>Checks whether property value is valid.</summary>
        /// <param name="value">The property value to be validated.</param>
        /// <returns>true if value is valid, otherwise it is false.</returns>
        public override bool IsValid(object value)
        {
            var d = (decimal)value;

            return MinValue <= d && d <= MaxValue;
        }
    }

}
