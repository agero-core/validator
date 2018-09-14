using Agero.Core.Validator.Helpers;
using System;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class StringValidateAttribute : ValidateAttribute
    {
        public StringValidateAttribute(string message)
            : base(message)
        {
            MinLength = -1;
            MaxLength = -1;
            CanBeNull = true;
            CanBeEmpty = true;
            RegEx = null;
        }

        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public bool CanBeNull { get; set; }
        public bool CanBeEmpty { get; set; }
        public string RegEx { get; set; }

        public override bool IsValid(object value)
        {
            return StringValidator.ValidateString((string)value, MinLength, MaxLength, CanBeNull, CanBeEmpty, RegEx);
        }
    }
}
