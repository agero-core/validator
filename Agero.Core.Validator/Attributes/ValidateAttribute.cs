using Agero.Core.Checker;
using System;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class ValidateAttribute : Attribute
    {
        protected ValidateAttribute(string message)
        {
            Check.ArgumentIsNullOrWhiteSpace(message, "message");

            Message = message;
        }

        public string Message { get; private set; }

        public string Key { get; set; }

        public abstract bool IsValid(object value);
    }
}
