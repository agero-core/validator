using Agero.Core.Checker;
using Agero.Core.Validator.Helpers;
using System;

namespace Agero.Core.Validator
{
    public class ValidationException : Exception, IValidationErrors
    {
        public ValidationException(object obj, ValidationError[] errors)
            : base(ExceptionMessageFormatter.CreateExceptionMessage(obj, errors))
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
