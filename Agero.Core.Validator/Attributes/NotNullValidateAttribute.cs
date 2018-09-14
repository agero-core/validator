using System;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NotNullValidateAttribute : ValidateAttribute
    {
        public NotNullValidateAttribute(string message)
            : base(message)
        {
        }

        public override bool IsValid(object value)
        {
            return value != null;
        }
    }
}
