using System;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ShortValidateAttribute : ValidateAttribute
    {
        public ShortValidateAttribute(string message)
            : base(message)
        {
            MinValue = short.MinValue;
            MaxValue = short.MaxValue;
        }

        public short MinValue { get; set; }
        public short MaxValue { get; set; }

        public override bool IsValid(object value)
        {
            var i = (short)value;

            return MinValue <= i && i <= MaxValue;
        }
    }
}
