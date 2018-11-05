using Agero.Core.Validator.Helpers;
using System;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>Validation attribute which specifies that property of <see cref="string"/> type needs to be validated by <see cref="ValidationHelper"/>.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class StringValidateAttribute : ValidateAttribute
    {
        /// <summary>Constructor</summary>
        /// <param name="message">Validation message which is used in validation error.</param>
        public StringValidateAttribute(string message)
            : base(message)
        {
        }

        /// <summary>Specifies minimum length of string. If it is less than 0, than this parameter is ignored for validation.</summary>
        public int MinLength { get; set; } = -1;
        
        /// <summary>Specifies maximum length of string. If it is less than 0, than this parameter is ignored for validation.</summary>
        public int MaxLength { get; set; } = -1;
        
        /// <summary>Specifies whether string can be null.</summary>
        public bool CanBeNull { get; set; } = true;
        
        /// <summary>Specifies whether string can be empty string.</summary>
        public bool CanBeEmpty { get; set; } = true;
        
        /// <summary>Specifies regular expression for string validation. If it is null, than this parameter is ignored for validation.</summary>
        public string RegEx { get; set; }

        /// <summary>Checks whether property value is valid.</summary>
        /// <param name="value">The property value to be validated.</param>
        /// <returns>true if value is valid, otherwise it is false.</returns>
        public override bool IsValid(object value)
        {
            return StringValidator.ValidateString((string)value, MinLength, MaxLength, CanBeNull, CanBeEmpty, RegEx);
        }
    }
}
