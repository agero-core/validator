using Agero.Core.Checker;
using Agero.Core.Validator.Helpers;
using System;
using System.Collections.Generic;

namespace Agero.Core.Validator
{
    /// <summary>Validation exception for particular untyped object.</summary>
    public class ValidationException : Exception, IValidationErrors
    {
        /// <summary>Constructor</summary>
        /// <param name="obj">Untyped object.</param>
        /// <param name="errors">Validation errors.</param>
        public ValidationException(object obj, IReadOnlyCollection<ValidationError> errors)
            : base(ExceptionMessageFormatter.CreateExceptionMessage(obj, errors))
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
