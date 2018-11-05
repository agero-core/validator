namespace Agero.Core.Validator
{
    /// <summary>Validation errors for particular typed object.</summary>
    public interface IValidationErrors<out T> : IValidationErrors
        where T : class
    {
        /// <summary>Typed object.</summary>
        T TypedObject { get; }
    }
}
