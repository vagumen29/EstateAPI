using FluentValidation;

namespace Application.Property.Commands.UpdateProperty;

public class UpdatePropertyCommandValidator: AbstractValidator<UpdatePropertyCommand>
{
    public UpdatePropertyCommandValidator()
    {
        RuleFor(command => command.IdProperty).GreaterThan(0).WithMessage("Invalid property ID.");
        RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(command => command.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(command => command.Year).InclusiveBetween(1900, DateTime.Now.Year).WithMessage("Year must be between 1900 and the current year.");
        RuleFor(command => command.IdOwner).GreaterThan(0).WithMessage("Invalid owner ID.");
    }
}
