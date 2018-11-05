using Agero.Core.Checker;
using System;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>Base validation attribute for validating value.</summary>
    public abstract class ValidateAttribute : Attribute
    {
        /// <summary>Constructor</summary>
        /// <param name="message">Validation message which is used in validation error.</param>
        protected ValidateAttribute(string message)
        {
            Check.ArgumentIsNullOrWhiteSpace(message, "message");

            Message = message;
        }

        /// <summary>Validation message which is used in validation error.</summary>
        public string Message { get; }

        /// <summary>Validation code which is used in validation error.</summary>
        public string Key { get; set; }

        /// <summary>Checks whether value is valid.</summary>
        /// <param name="value">The value to be validated.</param>
        /// <returns>true if value is valid, otherwise it is false.</returns>
        public abstract bool IsValid(object value);
    }
}
