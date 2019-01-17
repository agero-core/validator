# Validator

[![NuGet Version](http://img.shields.io/nuget/v/Agero.Core.Validator.svg?style=flat)](https://www.nuget.org/packages/Agero.Core.Validator/) 
[![NuGet Downloads](http://img.shields.io/nuget/dt/Agero.Core.Validator.svg?style=flat)](https://www.nuget.org/packages/Agero.Core.Validator/)

Model validation library for .NET applications.

## Usage:

Define model classes with properties and methods having validation attributes (e.g.,[IntValidate](./Agero.Core.Validator/Attributes/IntValidateAttribute.cs)) with constraints (e.g.,MinValue) according to your use case. An introductory example is shown below. For additional usage information refer to [QuickStartTests](./Agero.Core.Validator.Tests/QuickStartTests.cs).

```csharp
public class MyClass
{
    [IntValidate("Id must be greater than 0.", MinValue = 1)]
    public int Id { get; set; }

    [StringValidate("Text must have length between 2 and 100 characters.", MinLength = 2, MaxLength = 100)]
    public string Text { get; set; }

    [ComplexTypeValidate] // This attribute tells ValidationHelper to validate MyValueClass object
    [NotNullValidate("MyValue must be specified.")]
    public MyValueClass MyValue { get; set; }

    [ValidateMethod] // This attribute tells ValidationHelper to execute this method as part of model validation
    internal ValidationError ValidateTextHasOnlyLetters()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return null;
        
        if (Text.All(char.IsLetter))
            return null;
        
        return new ValidationError(nameof(Text), "Text must have only letters.");
    }
}

public class MyValueClass
{
    [NotNullValidate("Value must be specified.")]
    public object Value { get; set; }
}
```

Create an instance of **MyClass**.
```csharp
var myClass =
    new MyClass
    {
        Id = -1, // Invalid
        Text = "SomeText", // Valid
        MyValue = // Valid
            new MyValueClass
            {
                Value = null // Invalid
            }
    };
```

Then use [ValidationHelper](./Agero.Core.Validator/ValidationHelper.cs), which provides three type of methods to validate the object. 

* **Validate:** Returns **null**, if the object is valid, otherwise returns [IValidationErrors{T}](./Agero.Core.Validator/IValidationErrorsOfT.cs) or [IValidationErrors](./Agero.Core.Validator/IValidationErrors.cs).
* **IsValid:** Returns **true**, if the object is valid, otherwise returns **false**.
* **CheckIsValid:** Throws [IValidationException{T}](./Agero.Core.Validator/IValidationException<T>.cs) or [IValidationException](./Agero.Core.Validator/IValidationException.cs), if the object is invalid.

```csharp
// Instantiate ValidationHelper
var validator = new ValidationHelper();

// Validate using one of the following method
var validationErrors = validator.Validate(myClass);

var isMyClassValid = validator.IsValid(myClass);

validator.CheckIsValid(myClass);
```