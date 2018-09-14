using System;
using System.Globalization;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DecimalValidateAttribute : ValidateAttribute
    {
        private static readonly NumberFormatInfo _numberFormatInfo =
            new NumberFormatInfo { CurrencyDecimalSeparator = ".", NumberDecimalSeparator = "." };

        public DecimalValidateAttribute(string message)
            : base(message)
        {
            MinValueString = decimal.MinValue.ToString(_numberFormatInfo);
            MaxValueString = decimal.MaxValue.ToString(_numberFormatInfo);
        }

        public decimal MinValue
        {
            get { return decimal.Parse(MinValueString, _numberFormatInfo); }
        }

        public decimal MaxValue
        {
            get { return decimal.Parse(MaxValueString, _numberFormatInfo); }
        }

        public string MinValueString { get; set; }
        public string MaxValueString { get; set; }

        public override bool IsValid(object value)
        {
            var d = (decimal)value;

            return MinValue <= d && d <= MaxValue;
        }
    }

}
