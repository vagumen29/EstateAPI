using FluentValidation;

namespace Application.Property.Queries;

public class GetPropertiesQueryValidator: AbstractValidator<GetPropertiesQuery>
{
    public GetPropertiesQueryValidator()
    {
        RuleFor(query => query.Name).MaximumLength(100).When(query => !string.IsNullOrEmpty(query.Name)).WithMessage("Name must not exceed 100 characters.");
        RuleFor(query => query.Address).MaximumLength(200).When(query => !string.IsNullOrEmpty(query.Address)).WithMessage("Address must not exceed 200 characters.");
        RuleFor(query => query.Price).GreaterThan(0).When(query => query.Price.HasValue).WithMessage("Price must be greater than zero.");
        RuleFor(query => query.Year).InclusiveBetween(1900, DateTime.Now.Year).When(query => query.Year.HasValue).WithMessage("Year must be between 1990 and the current year.");
        RuleFor(query => query.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1.");
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(1).WithMessage("Page size must be greater than or equal to 1.");
    }
}
