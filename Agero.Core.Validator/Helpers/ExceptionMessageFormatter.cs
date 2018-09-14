using Agero.Core.Checker;
using System.Text;

namespace Agero.Core.Validator.Helpers
{
    internal static class ExceptionMessageFormatter
    {
        public static string CreateExceptionMessage(object obj, ValidationError[] errors)
        {
            Check.ArgumentIsNull(obj, "obj");
            Check.ArgumentIsNull(errors, "errors");
            Check.Argument(errors.Length > 0, "errors.Length > 0");

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
