using System;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class LongValidateAttribute : ValidateAttribute
    {
        public LongValidateAttribute(string message)
            : base(message)
        {
            MinValue = long.MinValue;
            MaxValue = long.MaxValue;
        }

        public long MinValue { get; set; }
        public long MaxValue { get; set; }

        public override bool IsValid(object value)
        {
            var i = (long)value;

            return MinValue <= i && i <= MaxValue;
        }
    }
}
