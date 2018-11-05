using System.Collections.Generic;
using Agero.Core.Checker;

namespace Agero.Core.Validator
{
    /// <summary>Validation errors for particular typed object.</summary>
    public class ValidationErrors<T> : ValidationErrors, IValidationErrors<T> 
        where T : class
    {
        /// <summary>Constructor</summary>
        /// <param name="obj">Typed object.</param>
        /// <param name="errors">Validation errors.</param>
        public ValidationErrors(T obj, IReadOnlyCollection<ValidationError> errors)
            : base(obj, errors)
        {
            Check.ArgumentIsNull(obj, "obj");

            TypedObject = obj;
        } 

        /// <summary>Typed object.</summary>
        public T TypedObject { get; }
    }
}
