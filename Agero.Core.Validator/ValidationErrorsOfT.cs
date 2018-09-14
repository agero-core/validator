using Agero.Core.Checker;

namespace Agero.Core.Validator
{
    public sealed class ValidationErrors<T> : ValidationErrors, IValidationErrors<T> 
        where T : class
    {
        public ValidationErrors(T obj, ValidationError[] errors)
            : base(obj, errors)
        {
            Check.ArgumentIsNull(obj, "obj");
            Check.ArgumentIsNull(errors, "errors");
            Check.Argument(errors.Length > 0, "errors.Length > 0");

            TypedObject = obj;
        } 

        public T TypedObject { get; private set; }
    }
}
