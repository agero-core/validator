using System;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IntValidateAttribute : ValidateAttribute
    {
        public IntValidateAttribute(string message)
            : base(message)
        {
            MinValue = int.MinValue;
            MaxValue = int.MaxValue;
        }

        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public override bool IsValid(object value)
        {
            var i = (int)value;

            return MinValue <= i && i <= MaxValue;
        }
    }
}
