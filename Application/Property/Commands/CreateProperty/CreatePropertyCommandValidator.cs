using FluentValidation;

namespace Application.Property.Commands.CreateProperty;

public class CreatePropertyCommandValidator: AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(command => command.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(command => command.CodeInternal).NotEmpty().WithMessage("Internal code is required.");
        RuleFor(command => command.Year).InclusiveBetween(1900, DateTime.Now.Year).WithMessage("Year must be between 1900 and the current year.");
        RuleFor(command => command.Owner).NotNull().WithMessage("Owner is required.");
        RuleFor(command => command.PropertyImages).NotEmpty().WithMessage("At least one property image must be provided.");
        RuleFor(command => command.PropertyTraces).NotEmpty().WithMessage("At least one property trace must be provided.");
    }
}
