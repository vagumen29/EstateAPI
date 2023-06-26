using Application.ApplicationUser.Queries.GetToken;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.ApplicationUser.Queries
{
    [TestFixture]
    public class GetTokenQueryValidatorTest
    {
        private GetTokenQueryValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new GetTokenQueryValidator();
        }

        [Test]
        public void Validate_WhenEmailIsEmpty_ShouldHaveValidationError()
        {
            // Arrange
            var query = new GetTokenQuery
            {
                Email = "", // Empty email
                Password = "password"
            };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.Email)
                .WithErrorMessage("Email is required.");
        }

        [Test]
        public void Validate_WhenEmailHasInvalidFormat_ShouldHaveValidationError()
        {
            // Arrange
            var query = new GetTokenQuery
            {
                Email = "invalidemail", // Invalid email format
                Password = "password"
            };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.Email)
                .WithErrorMessage("Invalid email format.");
        }

        [Test]
        public void Validate_WhenPasswordIsEmpty_ShouldHaveValidationError()
        {
            // Arrange
            var query = new GetTokenQuery
            {
                Email = "test@example.com",
                Password = "" // Empty password
            };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.Password)
                .WithErrorMessage("Password is required.");
        }

        [Test]
        public void Validate_WhenQueryIsValid_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var query = new GetTokenQuery
            {
                Email = "test@example.com",
                Password = "password"
            };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
