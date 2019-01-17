using System;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>Validation attribute which specifies that property of <see cref="short"/> type needs to be validated by <see cref="ValidationHelper"/>.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ShortValidateAttribute : ValidateAttribute
    {
        /// <summary>Constructor</summary>
        /// <param name="message">Validation message which is used in validation error.</param>
        public ShortValidateAttribute(string message)
            : base(message)
        {
            MinValue = short.MinValue;
            MaxValue = short.MaxValue;
        }

        /// <summary>Specifies optional minimum value.</summary>
        public short MinValue { get; set; }
        
        /// <summary>Specifies optional maximum value.</summary>
        public short MaxValue { get; set; }

        /// <summary>Checks whether property value is valid.</summary>
        /// <param name="value">The property value to be validated.</param>
        /// <returns>true if value is valid, otherwise it is false.</returns>
        public override bool IsValid(object value)
        {
            var i = (short)value;

            return MinValue <= i && i <= MaxValue;
        }
    }
}
