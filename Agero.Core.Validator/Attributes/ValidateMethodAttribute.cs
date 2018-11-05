using System;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>
    /// Validation attribute which marks method as validation method for <see cref="ValidationHelper"/>.
    /// Validation method must have no arguments.
    /// It also must return <see cref="ValidationError"/> if there is an validation error, otherwise it must return null. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateMethodAttribute : Attribute
    {
    }
}
