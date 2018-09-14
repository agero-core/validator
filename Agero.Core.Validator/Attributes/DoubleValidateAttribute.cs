using System;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DoubleValidateAttribute : ValidateAttribute
    {
        public DoubleValidateAttribute(string message)
            : base(message)
        {
            MinValue = double.MinValue;
            MaxValue = double.MaxValue;
        }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public override bool IsValid(object value)
        {
            var i = (double)value;

            return MinValue <= i && i <= MaxValue;
        }
    }
}
