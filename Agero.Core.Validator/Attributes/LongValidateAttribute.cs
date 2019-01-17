using System;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>Validation attribute which specifies that property of <see cref="long"/> type needs to be validated by <see cref="ValidationHelper"/>.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LongValidateAttribute : ValidateAttribute
    {
        /// <summary>Constructor</summary>
        /// <param name="message">Validation message which is used in validation error.</param>
        public LongValidateAttribute(string message)
            : base(message)
        {
            MinValue = long.MinValue;
            MaxValue = long.MaxValue;
        }

        /// <summary>Specifies optional minimum value.</summary>
        public long MinValue { get; set; }
        
        /// <summary>Specifies optional maximum value.</summary>
        public long MaxValue { get; set; }

        /// <summary>Checks whether property value is valid.</summary>
        /// <param name="value">The property value to be validated.</param>
        /// <returns>true if value is valid, otherwise it is false.</returns>
        public override bool IsValid(object value)
        {
            var i = (long)value;

            return MinValue <= i && i <= MaxValue;
        }
    }
}
