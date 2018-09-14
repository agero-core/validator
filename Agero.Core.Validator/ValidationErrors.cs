using Agero.Core.Checker;

namespace Agero.Core.Validator
{
    public class ValidationErrors : IValidationErrors
    {
        public ValidationErrors(object obj, ValidationError[] errors)
        {
            Check.ArgumentIsNull(obj, "obj");
            Check.ArgumentIsNull(errors, "errors");
            Check.Argument(errors.Length > 0, "errors.Length > 0");

            Object = obj;
            Errors = errors;
        }

        public object Object { get; private set; }

        public ValidationError[] Errors { get; private set; }
    }
}
