using System.Text.RegularExpressions;

namespace Agero.Core.Validator.Helpers
{
    public static class StringValidator
    {
        public static bool ValidateString(string str,
            int minLength = -1, int maxLength = -1, bool canBeNull = true, bool canBeEmpty = true, string regEx = null)
        {
            if (str == null)
                return canBeNull;

            if (str == string.Empty)
                return canBeEmpty;

            return
                (canBeEmpty || str != string.Empty) &&
                (minLength < 0 || str.Length >= minLength) &&
                (maxLength < 0 || str.Length <= maxLength) &&
                (regEx == null || Regex.IsMatch(str, regEx));
        }
    }
}
