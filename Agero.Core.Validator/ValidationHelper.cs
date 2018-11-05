using Agero.Core.Checker;
using Agero.Core.Validator.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Agero.Core.Validator
{
    /// <summary>Validation helper which validates object based on validation attributes applied to object's type.</summary>
    public class ValidationHelper : IValidationHelper
    {
        /// <summary>Validates object based on validation attributes applied to object's type and returns errors if validation failed.</summary>
        /// <param name="obj">The object to be validated.</param>
        /// <typeparam name="T">The object's type to be validated.</typeparam>
        /// <returns>Validation errors if object is valid, otherwise it is null.</returns>
        public IValidationErrors<T> Validate<T>(T obj)
             where T : class
        {
            Check.ArgumentIsNull(obj, nameof(obj));
            
            if (obj.GetType() != typeof(T))
                return null;
            
            var validationErrors = GetValidationErrors(obj);

            return validationErrors.Count > 0 ? new ValidationErrors<T>(obj, validationErrors) : null;
        }

        /// <summary>Validates object based on validation attributes applied to object's type and returns errors if validation failed.</summary>
        /// <param name="obj">The object to be validated.</param>
        /// <returns>Validation errors if object is valid, otherwise it is null.</returns>
        public IValidationErrors Validate(object obj)
        {
            Check.ArgumentIsNull(obj, nameof(obj));
            
            var validationErrors = GetValidationErrors(obj);

            return validationErrors.Count > 0 ? new ValidationErrors(obj, validationErrors) : null;
        }

        /// <summary>Returns whether object is valid based on validation attributes applied to object's type.</summary>
        /// <param name="obj">The object to be validated.</param>
        /// <typeparam name="T">The object's type to be validated.</typeparam>
        /// <returns>true if object is valid, otherwise it is false.</returns>
        public bool IsValid<T>(T obj)
            where T : class
        {
            Check.ArgumentIsNull(obj, nameof(obj));

            var errors = Validate(obj);

            return errors == null;
        }

        /// <summary>Returns whether object is valid based on validation attributes applied to object's type.</summary>
        /// <param name="obj">The object to be validated.</param>
        /// <returns>true if object is valid, otherwise it is false.</returns>
        public bool IsValid(object obj)
        {
            Check.ArgumentIsNull(obj, nameof(obj));

            var errors = Validate(obj);

            return errors == null;
        }

        /// <summary>
        /// Checks whether object is valid based on validation attributes applied to object's type.
        /// If object is invalid it throws <exception cref="ValidationException{T}"/> exception.
        /// </summary>
        /// <param name="obj">The object to be validated.</param>
        /// <typeparam name="T">The object's type to be validated.</typeparam>
        public void CheckIsValid<T>(T obj)
            where T : class
        {
            Check.ArgumentIsNull(obj, nameof(obj));

            var errors = Validate(obj);

            if (errors != null)
                throw new ValidationException<T>(errors.TypedObject, errors.Errors);
        }

        /// <summary>
        /// Checks whether object is valid based on validation attributes applied to object's type.
        /// If object is invalid it throws <exception cref="ValidationException"/> exception.
        /// </summary>
        /// <param name="obj">The object to be validated.</param>
        public void CheckIsValid(object obj)
        {
            Check.ArgumentIsNull(obj, nameof(obj));

            var errors = Validate(obj);

            if (errors != null)
                throw new ValidationException(errors.Object, errors.Errors);
        }

        private static IReadOnlyCollection<ValidationError> GetValidationErrors(object obj)
        {
            Check.ArgumentIsNull(obj, nameof(obj));

            const BindingFlags BINDING_ATTR = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            // Properties
            var propertiesValidationErrors = GetPropertyValidationErrors(obj, BINDING_ATTR);

            // Methods
            var methodsValidationErrors = GetMethodValidationErrors(obj, BINDING_ATTR);

            return 
                propertiesValidationErrors
                    .Concat(methodsValidationErrors)
                    .Distinct()
                    .ToArray();
        }

        private static IEnumerable<ValidationError> GetPropertyValidationErrors(object obj, BindingFlags bindingFlags)
        {
            Check.ArgumentIsNull(obj, nameof(obj));

            foreach (var property in obj.GetType().GetProperties(bindingFlags))
            {
                // Properties marked with ValidateAttribute
                var validateAttribute = property.GetCustomAttributes<ValidateAttribute>(true).SingleOrDefault();
                if (validateAttribute != null)
                {
                    var value = property.GetValue(obj, null);

                    var isValid = validateAttribute.IsValid(value);

                    if (!isValid)
                        yield return new ValidationError(validateAttribute.Key ?? property.Name, validateAttribute.Message);
                }
                
                // Properties marked with ComplexTypeValidateAttribute
                var validateComplexTypeAttribute = property.GetCustomAttributes<ComplexTypeValidateAttribute>(true).SingleOrDefault();
                if (validateComplexTypeAttribute != null)
                {
                    var value = property.GetValue(obj, null);
                    if (value != null)
                    {
                        // Property is collection?
                        if (value is IEnumerable collectionValues)
                        {
                            foreach (var collectionValue in collectionValues)
                            {
                                var validationErrors = GetValidationErrors(collectionValue);

                                foreach (var validationError in validationErrors)
                                    yield return validationError;
                            }
                        }
                        else
                        {
                            var validationErrors = GetValidationErrors(value);

                            foreach (var validationError in validationErrors)
                                yield return validationError;
                        }
                    }
                }
            }
        }

        private static IEnumerable<ValidationError> GetMethodValidationErrors(object obj, BindingFlags bindingFlags)
        {
            Check.ArgumentIsNull(obj, nameof(obj));
            
            return 
                from method in obj.GetType().GetMethods(bindingFlags)
                let attribute = method.GetCustomAttributes<ValidateMethodAttribute>(true).SingleOrDefault()
                where attribute != null
                      && !method.GetParameters().Any()
                      && method.ReturnType == typeof(ValidationError)
                let result = (ValidationError)method.Invoke(obj, null)
                where result != null
                select result;
        }
    }
}
