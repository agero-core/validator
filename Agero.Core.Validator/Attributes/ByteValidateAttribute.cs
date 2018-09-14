using System;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ByteValidateAttribute : ValidateAttribute
    {
        public ByteValidateAttribute(string message)
            : base(message)
        {
            MinValue = byte.MinValue;
            MaxValue = byte.MaxValue;
        }

        public byte MinValue { get; set; }
        public byte MaxValue { get; set; }

        public override bool IsValid(object value)
        {
            var i = (byte)value;

            return MinValue <= i && i <= MaxValue;
        }
    }
}
