﻿using Agero.Core.Checker;
using System.Collections.Generic;

namespace Agero.Core.Validator
{
    /// <summary>Validation exception for particular typed object.</summary>
    public class ValidationException<T> : ValidationException, IValidationErrors<T> 
        where T : class
    {
        /// <summary>Constructor</summary>
        /// <param name="obj">Typed object.</param>
        /// <param name="errors">Validation errors.</param>
        public ValidationException(T obj, IReadOnlyCollection<ValidationError> errors)
            : base(obj, errors)
        {
            Check.ArgumentIsNull(obj, "obj");

            TypedObject = obj;
        }

        /// <summary>Typed object.</summary>
        public T TypedObject { get; }
    }
}
