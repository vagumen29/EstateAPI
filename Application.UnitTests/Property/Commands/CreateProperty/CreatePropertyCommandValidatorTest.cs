using Application.Dto;
using Application.Property.Commands.CreateProperty;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Property.Commands.CreateProperty;

public class CreatePropertyCommandValidatorTest
{
    private CreatePropertyCommandValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new CreatePropertyCommandValidator();
    }

    [Test]
    public void Validate_WhenNameIsEmpty_ShouldHaveValidationError()
    {
        var command = new CreatePropertyCommand
        {
            Name = "",          
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("Name is required.");
    }

    [Test]
    public void Validate_WhenAddressIsEmpty_ShouldHaveValidationError()
    {
        var command = new CreatePropertyCommand
        {
            Name = "Property 33",
            Address = "",                   
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Address)
            .WithErrorMessage("Address is required.");
    }

    [Test]
    public void Validate_WhenPriceIsZero_ShouldHaveValidationError()
    {
        var command = new CreatePropertyCommand
        {
            Name = "Property 100",
            Address = "100011 Thomson St",
            Price = 0,
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Price)
            .WithErrorMessage("Price must be greater than zero.");
    }

    [Test]
    public void Validate_WhenCodeInternalIsEmpty_ShouldHaveValidationError()
    {
        var command = new CreatePropertyCommand
        {
            Name = "Property 2",
            Address = "234 Queen St",
            Price = 100000,
            CodeInternal = "",             
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.CodeInternal)
            .WithErrorMessage("Internal code is required.");
    }

    [Test]
    public void Validate_WhenYearIsLessThan1900_ShouldHaveValidationError()
    {
        var command = new CreatePropertyCommand
        {
            Name = "Property 34",
            Address = "San Patrick St",
            Price = 100000,
            CodeInternal = "IUY876",
            Year = 1899,          
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Year)
            .WithErrorMessage("Year must be between 1900 and the current year.");
    }

    [Test]
    public void Validate_WhenYearIsGreaterThanCurrentYear_ShouldHaveValidationError()
    {
        var command = new CreatePropertyCommand
        {
            Name = "Property 1",
            Address = "Malibu St",
            Price = 200000,
            CodeInternal = "HST619",
            Year = DateTime.Now.Year + 1,               
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Year)
            .WithErrorMessage("Year must be between 1900 and the current year.");
    }

    [Test]
    public void Validate_WhenOwnerIsNull_ShouldHaveValidationError()
    {
        var command = new CreatePropertyCommand
        {
            Name = "Property 1",
            Address = "Bulevar St",
            Price = 100000,
            CodeInternal = "HST619",
            Year = 2022,
            Owner = null,           
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Owner)
            .WithErrorMessage("Owner is required.");
    }

    [Test]
    public void Validate_WhenPropertyImagesIsEmpty_ShouldHaveValidationError()
    {
        var command = new CreatePropertyCommand
        {
            Name = "Property 1",
            Address = "Bulevar St",
            Price = 100000,
            CodeInternal = "HST619",
            Year = 2022,
            Owner = new OwnerDto(),
            PropertyImages = new List<PropertyImageDto>(),          
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.PropertyImages)
            .WithErrorMessage("At least one property image must be provided.");
    }

    [Test]
    public void Validate_WhenPropertyTracesIsEmpty_ShouldHaveValidationError()
    {
        var command = new CreatePropertyCommand
        {
            Name = "Property 1",
            Address = "Bulevar St",
            Price = 100000,
            CodeInternal = "HST619",
            Year = 2022,
            Owner = new OwnerDto(),
            PropertyImages = new List<PropertyImageDto>
                {
                    new PropertyImageDto()
                },
            PropertyTraces = new List<PropertyTraceDto>(),             
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.PropertyTraces)
            .WithErrorMessage("At least one property trace must be provided.");
    }

    [Test]
    public void Validate_WhenCommandIsValid_ShouldNotHaveValidationErrors()
    {
        var command = new CreatePropertyCommand
        {
            Name = "Property 1",
            Address = "Bulevar St",
            Price = 100000,
            CodeInternal = "HST619",
            Year = 2022,
            Owner = new OwnerDto(),
            PropertyImages = new List<PropertyImageDto>
                {
                    new PropertyImageDto()
                },
            PropertyTraces = new List<PropertyTraceDto>
                {
                    new PropertyTraceDto()
                },
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
