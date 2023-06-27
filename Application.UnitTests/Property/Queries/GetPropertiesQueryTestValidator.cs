using Application.Property.Queries;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Property.Queries;

[TestFixture]
public class GetPropertiesQueryValidatorTest
{

    private GetPropertiesQueryValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new GetPropertiesQueryValidator();
    }

    [Test]
    public void Validate_WithNameExceedingMaxLength_ReturnsValidationError()
    {
        var query = new GetPropertiesQuery
        {
            Name = new string('A', 101)
        };

        var result = _validator.TestValidate(query);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Name must not exceed 100 characters."));
    }

    [Test]
    public void Validate_WithAddressExceedingMaxLength_ReturnsValidationError()
    {
        var query = new GetPropertiesQuery
        {
            Address = new string('A', 201)
        };

        var result = _validator.TestValidate(query);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Address must not exceed 200 characters."));
    }

    [Test]
    public void Validate_WithNegativePrice_ReturnsValidationError()
    {

        var query = new GetPropertiesQuery
        {
            Price = -100
        };


        var result = _validator.TestValidate(query);

        Assert.IsFalse(result.IsValid);
        Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Price must be greater than zero."));
    }

    [Test]
    public void Validate_WithInvalidYear_ReturnsValidationError()
    {
        var query = new GetPropertiesQuery
        {
            Year = 1800
        };

        var result = _validator.Validate(query);

        Assert.IsFalse(result.IsValid);
    }

    [Test]
    public void Validate_WithValidQuery_ReturnsValidationSuccess()
    {
        var query = new GetPropertiesQuery
        {
            Name = "Property 3",
            Address = "345 Main St",
            Price = 200000,
            Year = 2023
        };

        var result = _validator.Validate(query);

        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Empty);
    }

}
