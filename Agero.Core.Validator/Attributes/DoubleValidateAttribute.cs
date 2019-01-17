using System;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>Validation attribute which specifies that property of <see cref="double"/> type needs to be validated by <see cref="ValidationHelper"/>.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DoubleValidateAttribute : ValidateAttribute
    {
        /// <summary>Constructor</summary>
        /// <param name="message">Validation message which is used in validation error.</param>
        public DoubleValidateAttribute(string message)
            : base(message)
        {
            MinValue = double.MinValue;
            MaxValue = double.MaxValue;
        }

        /// <summary>Specifies optional minimum value.</summary>
        public double MinValue { get; set; }
        
        /// <summary>Specifies optional maximum value.</summary>
        public double MaxValue { get; set; }

        /// <summary>Checks whether property value is valid.</summary>
        /// <param name="value">The property value to be validated.</param>
        /// <returns>true if value is valid, otherwise it is false.</returns>
        public override bool IsValid(object value)
        {
            var i = (double)value;

            return MinValue <= i && i <= MaxValue;
        }
    }
}
