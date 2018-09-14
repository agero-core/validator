using Agero.Core.Checker;
using System;

namespace Agero.Core.Validator
{
    public sealed class ValidationError : IEquatable<ValidationError>
    {
        public ValidationError(string key, string message)
        {
            Check.ArgumentIsNullOrWhiteSpace(key, "key");
            Check.ArgumentIsNullOrWhiteSpace(message, "message");

            Key = key;
            Message = message;
        }

        public string Key { get; private set; }
        public string Message { get; private set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ValidationError);
        }

        public override int GetHashCode()
        {
            return 
                Key.GetHashCode() ^ 
                Message.GetHashCode();
        }

        public bool Equals(ValidationError other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return
                other.Key == Key &&
                other.Message == Message;
        }
    }
}
