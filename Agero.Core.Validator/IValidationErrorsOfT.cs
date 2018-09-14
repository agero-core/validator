namespace Agero.Core.Validator
{
    public interface IValidationErrors<out T> : IValidationErrors
        where T : class
    {
        T TypedObject { get; }
    }
}
