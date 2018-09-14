namespace Agero.Core.Validator
{
    public interface IValidationHelper
    {
        IValidationErrors<T> Validate<T>(T obj)
            where T : class;

        IValidationErrors Validate(object obj);

        bool IsValid<T>(T obj)
            where T : class;

        bool IsValid(object obj);

        void CheckIsValid<T>(T obj)
           where T : class;

        void CheckIsValid(object obj);
    }
}
