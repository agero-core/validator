using System;

namespace Agero.Core.Validator.Attributes
{
    /// <summary>Validation attribute which tells that property type has validation attributes that needs to be evaluated by <see cref="ValidationHelper"/>.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ComplexTypeValidateAttribute : Attribute
    {
    }
}
