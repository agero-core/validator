using Agero.Core.Checker;
using System;
using System.Linq;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>
    /// Validation attribute which specifies that property of <see cref="string"/> type needs to be validated against enumeration values by <see cref="ValidationHelper"/>.
    /// It uses invariant culture for matching.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EnumValidateAttribute : ValidateAttribute
    {
        /// <summary>Constructor</summary>
        /// <param name="message">Validation message which is used in validation error.</param>
        /// <param name="enumType">Enumeration type to be validated against.</param>
        public EnumValidateAttribute(string message, Type enumType)
            : base(message)
        {
            Check.ArgumentIsNull(enumType, "enumType");
            Check.Argument(enumType.IsEnum, "enumType.IsEnum");

            EnumType = enumType;

            CanBeNull = false;
            IgnoreCase = false;
        }

        /// <summary>Enumeration type to be validated against.</summary>
        public Type EnumType { get; }

        /// <summary>Spesifies whether value can be null. Default is false.</summary>
        public bool CanBeNull { get; set; }

        /// <summary>Spesifies whether value needs to be validated against enumeration values using case-insesitive matching. Default is false.</summary>
        public bool IgnoreCase { get; set; }

        /// <summary>Checks whether property value is valid.</summary>
        /// <param name="value">The property value to be validated.</param>
        /// <returns>true if value is valid, otherwise it is false.</returns>
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
