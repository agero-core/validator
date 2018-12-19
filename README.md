# Validator

[![NuGet Version](http://img.shields.io/nuget/v/Agero.Core.Validator.svg?style=flat)](https://www.nuget.org/packages/Agero.Core.Validator/) 
[![NuGet Downloads](http://img.shields.io/nuget/dt/Agero.Core.Validator.svg?style=flat)](https://www.nuget.org/packages/Agero.Core.Validator/)

Model validation library for .NET applications.

## Usage:

Define model classes with properties having validation attributes (e.g.,**IntValidate**) having constrainsts (e.g.,**MinValue**) according to your use case. An small is shown below. For additional usage information refer [QuickStartTests](./Agero.Core.Validator.Tests/QuickStartTests.cs).

```csharp
public class MyClass
{
    [IntValidate("Id must be greater than 0.", MinValue = 1)]
    public int Id { get; set; }

    [StringValidate("Text must have lenghth between 2 and 100 characters.", MinLength = 2, MaxLength = 100)]
    public string Text { get; set; }

    [ComplexTypeValidate] // This attribute tells ValidationHelper to validate MyValueClass object
    [NotNullValidate("MyValue must be specified.")]
    public MyValueClass MyValue { get; set; }
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
        Text = "Some text", // Valid
        MyValue = // Valid
            new MyValueClass
            {
                Value = null // Invalid
            }
    };
```

**Validator**, provides three methods to validate the object. 

* **Validate:** Returns a list of validation errors.
* **IsValid:** Returns a boolean, representing if the object is valid.
* **CheckIsValid:** Throws an exception [ValidationException](./Agero.Core.Validator/ValidationException.cs) if the object is invalid.

```csharp
var validator = new ValidationHelper();

var validationErrors = validator.Validate(myClass);

var isMyclassValid = validator.IsValid(myClass);

validator.CheckIsValid(myClass);
```

