using System.Collections.Generic;
using Agero.Core.Checker;

namespace Agero.Core.Validator
{
    /// <summary>Validation errors for particular untyped object.</summary>
    public class ValidationErrors : IValidationErrors
    {
        /// <summary>Constructor</summary>
        /// <param name="obj">Untyped object.</param>
        /// <param name="errors">Validation errors.</param>
        public ValidationErrors(object obj, IReadOnlyCollection<ValidationError> errors)
        {
            Check.ArgumentIsNull(obj, "obj");
            Check.ArgumentIsNull(errors, "errors");
            Check.Argument(errors.Count > 0, "errors.Count > 0");

            Object = obj;
            Errors = errors;
        }

        /// <summary>Untyped object.</summary>
        public object Object { get; }

        /// <summary>Validation errors.</summary>
        public IReadOnlyCollection<ValidationError> Errors { get; }
    }
}
