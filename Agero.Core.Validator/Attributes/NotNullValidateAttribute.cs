using System;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>Validation attribute which specifies that non-nullable property needs to be checked for null by <see cref="ValidationHelper"/>.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullValidateAttribute : ValidateAttribute
    {
        /// <summary>Constructor</summary>
        /// <param name="message">Validation message which is used in validation error.</param>
        public NotNullValidateAttribute(string message)
            : base(message)
        {
        }

        /// <summary>Validates property value.</summary>
        /// <param name="value">Property value to be validated.</param>
        /// <returns>true if value is valid, otherwise it is false.</returns>
        public override bool IsValid(object value)
        {
            return value != null;
        }
    }
}
