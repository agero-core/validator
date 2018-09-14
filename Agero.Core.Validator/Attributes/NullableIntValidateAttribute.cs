using System;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NullableIntValidateAttribute : IntValidateAttribute
    {
        public NullableIntValidateAttribute(string message)
            : base(message)
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            return base.IsValid(value);
        }
    }
}
