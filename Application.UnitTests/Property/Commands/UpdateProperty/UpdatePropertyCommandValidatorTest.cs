using Application.Property.Commands.UpdateProperty;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Property.Commands.UpdateProperty;

[TestFixture]
public class UpdatePropertyCommandValidatorTest
{
    private UpdatePropertyCommandValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new UpdatePropertyCommandValidator();
    }

    [Test]
    public void Validate_WhenIdPropertyIsZero()
    {
        var command = new UpdatePropertyCommand
        {
            IdProperty = 0,             
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.IdProperty)
            .WithErrorMessage("Invalid property ID.");
    }

    [Test]
    public void Validate_WhenPriceIsZero()
    {
        var command = new UpdatePropertyCommand
        {
            IdProperty = 1,
            Price = 0,

        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Price)
            .WithErrorMessage("Price must be greater than zero.");
    }

    [Test]
    public void Validate_WhenNameIsEmpty()
    {
        var command = new UpdatePropertyCommand
        {
            IdProperty = 1,
            Price = 100000,
            Name = "",

        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("Name is required.");
    }

    [Test]
    public void Validate_WhenAddressIsEmpty()
    {
        var command = new UpdatePropertyCommand
        {
            IdProperty = 1,
            Price = 100000,
            Name = "Property 1",
            Address = "",          
        };

        var result = _validator.TestValidate(command);

         result.ShouldHaveValidationErrorFor(c => c.Address)
            .WithErrorMessage("Address is required.");
    }

    [Test]
    public void Validate_WhenYearIsLessThan1900()
    {
        var command = new UpdatePropertyCommand
        {
            IdProperty = 1,
            Price = 100000,
            Name = "Property 1",
            Address = "123 Main St",
            Year = 1899,           
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Year)
            .WithErrorMessage("Year must be between 1900 and the current year.");
    }

    [Test]
    public void Validate_WhenYearIsGreaterThanCurrentYear()
    {
        var command = new UpdatePropertyCommand
        {
            IdProperty = 1,
            Price = 100000,
            Name = "Property 1",
            Address = "123 Main St",
            Year = DateTime.Now.Year + 1,            
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Year)
            .WithErrorMessage("Year must be between 1900 and the current year.");
    }

    [Test]
    public void Validate_WhenIdOwnerIsZero()
    {
        var command = new UpdatePropertyCommand
        {
            IdProperty = 1,
            Price = 100000,
            Name = "Property 1",
            Address = "123 Main St",
            Year = 2022,
            IdOwner = 0,            
        };

         var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.IdOwner)
            .WithErrorMessage("Invalid owner ID.");
    }

    [Test]
    public void Validate_WhenCommandIsValid()
    {
        var command = new UpdatePropertyCommand
        {
            IdProperty = 1,
            Price = 100000,
            Name = "Property 1",
            Address = "123 Main St",
            Year = 2022,
            IdOwner = 1,
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
