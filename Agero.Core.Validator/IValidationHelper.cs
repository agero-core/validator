namespace Agero.Core.Validator
{
    /// <summary>Validation helper which validates object based on validation attributes applied to object's type.</summary>
    public interface IValidationHelper
    {
        /// <summary>Validates object based on validation attributes applied to object's type and returns errors if validation failed.</summary>
        /// <param name="obj">The object to be validated.</param>
        /// <typeparam name="T">The object's type to be validated.</typeparam>
        /// <returns>Validation errors if object is valid, otherwise it is null.</returns>
        IValidationErrors<T> Validate<T>(T obj)
            where T : class;

        /// <summary>Validates object based on validation attributes applied to object's type and returns errors if validation failed.</summary>
        /// <param name="obj">The object to be validated.</param>
        /// <returns>Validation errors if object is valid, otherwise it is null.</returns>
        IValidationErrors Validate(object obj);

        /// <summary>Returns whether object is valid based on validation attributes applied to object's type.</summary>
        /// <param name="obj">The object to be validated.</param>
        /// <typeparam name="T">The object's type to be validated.</typeparam>
        /// <returns>true if object is valid, otherwise it is false.</returns>
        bool IsValid<T>(T obj)
            where T : class;

        /// <summary>Returns whether object is valid based on validation attributes applied to object's type.</summary>
        /// <param name="obj">The object to be validated.</param>
        /// <returns>true if object is valid, otherwise it is false.</returns>
        bool IsValid(object obj);

        /// <summary>
        /// Checks whether object is valid based on validation attributes applied to object's type.
        /// If object is invalid it throws <exception cref="ValidationException{T}"/> exception.
        /// </summary>
        /// <param name="obj">The object to be validated.</param>
        /// <typeparam name="T">The object's type to be validated.</typeparam>
        void CheckIsValid<T>(T obj)
           where T : class;

        /// <summary>
        /// Checks whether object is valid based on validation attributes applied to object's type.
        /// If object is invalid it throws <exception cref="ValidationException"/> exception.
        /// </summary>
        /// <param name="obj">The object to be validated.</param>
        void CheckIsValid(object obj);
    }
}
