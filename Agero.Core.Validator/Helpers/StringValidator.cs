using System.Text.RegularExpressions;

namespace Agero.Core.Validator.Helpers
{
    /// <summary>Helper for validating <see cref="string"/>.</summary>
    public static class StringValidator
    {
        /// <summary>Validates given <see cref="string"/> based on provided parameters.</summary>
        /// <param name="str">String to be validated.</param>
        /// <param name="minLength">Specifies minimum length of string. If it is less than 0, than this parameter is ignored for validation.</param>
        /// <param name="maxLength">Specifies maximum length of string. If it is less than 0, than this parameter is ignored for validation.</param>
        /// <param name="canBeNull">Specifies whether string can be null.</param>
        /// <param name="canBeEmpty">Specifies whether string can be empty string.</param>
        /// <param name="regEx">Specifies regular expression for string validation. If it is null, than this parameter is ignored for validation.</param>
        /// <returns>true if object is valid, otherwise it is false.</returns>
        public static bool ValidateString(string str,
            int minLength = -1, int maxLength = -1, bool canBeNull = true, bool canBeEmpty = true, string regEx = null)
        {
            if (str == null)
                return canBeNull;

            if (str == string.Empty)
                return canBeEmpty;

            return
                (minLength < 0 || str.Length >= minLength) &&
                (maxLength < 0 || str.Length <= maxLength) &&
                (regEx == null || Regex.IsMatch(str, regEx));
        }
    }
}
