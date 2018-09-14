namespace Agero.Core.Validator
{
    public interface IValidationErrors
    {
        ValidationError[] Errors { get; }

        object Object { get; }
    }
}
