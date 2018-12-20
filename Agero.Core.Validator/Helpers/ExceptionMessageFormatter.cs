using Agero.Core.Checker;
using System.Collections.Generic;
using System.Text;

namespace Agero.Core.Validator.Helpers
{
    internal static class ExceptionMessageFormatter
    {
        public static string CreateExceptionMessage(object obj, IReadOnlyCollection<ValidationError> errors)
        {
            Check.ArgumentIsNull(obj, "obj");
            Check.ArgumentIsNull(errors, "errors");
            Check.Argument(errors.Count > 0, "errors.Count > 0");

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Invalid object: ");
            stringBuilder.AppendLine(obj.ToString());
            stringBuilder.AppendLine("Errors: ");

            foreach (var error in errors)
            {
                stringBuilder.Append("Key: ");
                stringBuilder.Append(error.Key);
                stringBuilder.Append(" Message: ");
                stringBuilder.AppendLine(error.Message);
            }

            return stringBuilder.ToString();
        }
    }
}
