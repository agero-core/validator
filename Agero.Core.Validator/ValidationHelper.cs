using Agero.Core.Checker;
using Agero.Core.Validator.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Agero.Core.Validator
{
    public class ValidationHelper : IValidationHelper
    {
        public IValidationErrors<T> Validate<T>(T obj)
             where T : class
        {
            Check.ArgumentIsNull(obj, "obj");

            if (obj.GetType() != typeof (T))
                return null;

            var validationErrors = 
                GetValidationErrors(obj)
                    .ToArray();

            return validationErrors.Length > 0 ? new ValidationErrors<T>(obj, validationErrors) : null;
        }

        public IValidationErrors Validate(object obj)
        {
            Check.ArgumentIsNull(obj, "obj");
            
            var validationErrors =
                GetValidationErrors(obj)
                    .ToArray();

            return validationErrors.Length > 0 ? new ValidationErrors(obj, validationErrors) : null;
        }

        public bool IsValid<T>(T obj)
            where T : class
        {
            Check.ArgumentIsNull(obj, "obj");

            var errors = Validate(obj);

            return errors == null;
        }

        public bool IsValid(object obj)
        {
            Check.ArgumentIsNull(obj, "obj");

            var errors = Validate(obj);

            return errors == null;
        }

        public void CheckIsValid<T>(T obj)
            where T : class
        {
            Check.ArgumentIsNull(obj, "obj");

            var errors = Validate(obj);

            if (errors != null)
                throw new ValidationException<T>(errors.TypedObject, errors.Errors);
        }

        public void CheckIsValid(object obj)
        {
            Check.ArgumentIsNull(obj, "obj");

            var errors = Validate(obj);

            if (errors != null)
                throw new ValidationException(errors.Object, errors.Errors);
        }

        private IEnumerable<ValidationError> GetValidationErrors(object obj)
        {
            Check.ArgumentIsNull(obj, "obj");

            const BindingFlags BINDING_ATTR =
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static;

            // Properties
            var propertiesValidationErrors = GetPropertiesValidationErrors(obj, BINDING_ATTR);

            // Methods
            var methodsValidationErrors =
                from method in obj.GetType().GetMethods(BINDING_ATTR)
                let attribute =
                    method
                        .GetCustomAttributes<ValidateMethodAttribute>(true)
                        .SingleOrDefault()
                where attribute != null
                      && !method.GetParameters().Any()
                      && method.ReturnType == typeof(ValidationError)
                let result = (ValidationError)method.Invoke(obj, null)
                where result != null
                select result;

            return 
                propertiesValidationErrors
                    .Concat(methodsValidationErrors)
                    .Distinct();
        }

        private IEnumerable<ValidationError> GetPropertiesValidationErrors(object obj, BindingFlags bindingFlags)
        {
            Check.ArgumentIsNull(obj, "obj");

            foreach (var property in obj.GetType().GetProperties(bindingFlags))
            {
                var validateAttribute =
                    property
                        .GetCustomAttributes<ValidateAttribute>(true)
                        .SingleOrDefault();
                if (validateAttribute != null)
                {
                    var value = property.GetValue(obj, null);

                    var isValid = validateAttribute.IsValid(value);

                    if (!isValid)
                        yield return new ValidationError(validateAttribute.Key ?? property.Name, validateAttribute.Message);
                }
                
                var validateComplexTypeAttribute = 
                    property
                        .GetCustomAttributes<ComplexTypeValidateAttribute>(true)
                        .SingleOrDefault();
                if (validateComplexTypeAttribute != null)
                {
                    var value = property.GetValue(obj, null);
                    if (value != null)
                    {
                        var collectionValues = value as IEnumerable;
                        if (collectionValues != null)
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
    }
}
