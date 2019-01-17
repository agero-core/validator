using System;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>
    /// Validation attribute which specifies that property of nullable <see cref="decimal"/> type needs to be validated by <see cref="ValidationHelper"/>.
    /// If value is null, then validation is passed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NullableDecimalValidateAttribute : DecimalValidateAttribute
    {
        /// <summary>Constructor</summary>
        /// <param name="message">Validation message which is used in validation error.</param>
        public NullableDecimalValidateAttribute(string message)
            : base(message)
        {
        }

        /// <summary>Checks whether property value is valid.</summary>
        /// <param name="value">The property value to be validated.</param>
        /// <returns>true if value is valid, otherwise it is false.</returns>
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            return base.IsValid(value);
        }
    }
}
