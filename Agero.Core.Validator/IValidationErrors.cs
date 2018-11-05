using System.Collections.Generic;

namespace Agero.Core.Validator
{
    /// <summary>Validation errors for particular untyped object.</summary>
    public interface IValidationErrors
    {
        /// <summary>Validation errors.</summary>
        IReadOnlyCollection<ValidationError> Errors { get; }

        /// <summary>Untyped object.</summary>
        object Object { get; }
    }
}
