# Validator

[![NuGet Version](http://img.shields.io/nuget/v/Agero.Core.Validator.svg?style=flat)](https://www.nuget.org/packages/Agero.Core.Validator/) 
[![NuGet Downloads](http://img.shields.io/nuget/dt/Agero.Core.Validator.svg?style=flat)](https://www.nuget.org/packages/Agero.Core.Validator/)

Model validation library for .NET applications.

## Usage:

Define model classes with properties having validation attributes (e.g.,**IntValidate**) having constrainsts (e.g.,**MinValue**) according to your use case. An small example is shown below. For additional usage information refer [QuickStartTests](./Agero.Core.Validator.Tests/QuickStartTests.cs).

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

    [ValidateMethod] // This attribute tells ValidationHelper to execute this method as part of model validation
    internal ValidationError ValidateValue()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return new ValidationError(nameof(Text), "Text must be specified.");

        if(Id > 1000 && Id < 2000)
            return new ValidationError(nameof(Text), "Id cannot have a value between 1000 and 2000.");

        return null;
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
        Text = "Some text", // Valid
        MyValue = // Valid
            new MyValueClass
            {
                Value = null // Invalid
            }
    };
```

**Validator**, provides three methods to validate the object. 

* **Validate:** Returns **null**, if the object is valid, else returns [ValidationErrors](./Agero.Core.Validator/ValidationErrors.cs).
* **IsValid:** Returns True, if the object is valid, else returns False.
* **CheckIsValid:** Throws [ValidationException](./Agero.Core.Validator/ValidationException.cs), if the object is invalid.

```csharp
//Instantiate ValidationHelper
var validator = new ValidationHelper();

//Validate using preferred method
var validationErrors = validator.Validate(myClass);

var isMyclassValid = validator.IsValid(myClass);

validator.CheckIsValid(myClass);
```