using System;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>Validation attribute which specifies that property of <see cref="int"/> type needs to be validated by <see cref="ValidationHelper"/>.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IntValidateAttribute : ValidateAttribute
    {
        /// <summary>Constructor</summary>
        /// <param name="message">Validation message which is used in validation error.</param>
        public IntValidateAttribute(string message)
            : base(message)
        {
            MinValue = int.MinValue;
            MaxValue = int.MaxValue;
        }

        /// <summary>Specifies optional minimum value.</summary>
        public int MinValue { get; set; }
        
        /// <summary>Specifies optional maximum value.</summary>
        public int MaxValue { get; set; }

        /// <summary>Checks whether property value is valid.</summary>
        /// <param name="value">The property value to be validated.</param>
        /// <returns>true if value is valid, otherwise it is false.</returns>
        public override bool IsValid(object value)
        {
            var i = (int)value;

            return MinValue <= i && i <= MaxValue;
        }
    }
}
