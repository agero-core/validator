using Agero.Core.Checker;
using System;

namespace Agero.Core.Validator
{
    /// <summary>Validation error.</summary>
    public class ValidationError : IEquatable<ValidationError>
    {
        /// <summary>Constructor</summary>
        /// <param name="key">Validation error code.</param>
        /// <param name="message">Validation error message.</param>
        public ValidationError(string key, string message)
        {
            Check.ArgumentIsNullOrWhiteSpace(key, nameof(key));
            Check.ArgumentIsNullOrWhiteSpace(message, nameof(message));

            Key = key;
            Message = message;
        }

        /// <summary>Validation error code.</summary>
        public string Key { get; }
        
        /// <summary>Validation error message.</summary>
        public string Message { get; }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is ValidationError error && Equals(error);
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Key.GetHashCode() * 397) ^ Message.GetHashCode();
            }
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        public bool Equals(ValidationError other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Key, other.Key) && string.Equals(Message, other.Message);
        }

        /// <summary>== operator.</summary>
        public static bool operator ==(ValidationError left, ValidationError right)
        {
            return Equals(left, right);
        }

        /// <summary>!= operator.</summary>
        public static bool operator !=(ValidationError left, ValidationError right)
        {
            return !Equals(left, right);
        }
    }
}
