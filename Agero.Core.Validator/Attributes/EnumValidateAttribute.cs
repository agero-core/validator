using Agero.Core.Checker;
using System;
using System.Linq;

namespace Agero.Core.Validator.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EnumValidateAttribute : ValidateAttribute
    {
        public EnumValidateAttribute(string message, Type enumType)
            : base(message)
        {
            Check.ArgumentIsNull(enumType, "enumType");
            Check.Argument(enumType.IsEnum, "enumType.IsEnum");

            EnumType = enumType;

            CanBeNull = false;
            IgnoreCase = false;
        }

        public Type EnumType { get; private set; }

        public bool CanBeNull { get; set; }

        public bool IgnoreCase { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
                return CanBeNull;

            var str = (string)value;
            if (string.IsNullOrWhiteSpace(str))
                return false;

            return EnumType.GetEnumNames().Contains(str, IgnoreCase ? StringComparer.InvariantCultureIgnoreCase : StringComparer.InvariantCulture);
        }
    }
}
