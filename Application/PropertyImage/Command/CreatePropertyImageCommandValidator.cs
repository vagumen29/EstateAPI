using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PropertyImage.Command;

public class CreatePropertyImageCommandValidator: AbstractValidator<CreatePropertyImageCommand>
{
    public CreatePropertyImageCommandValidator()
    {
        RuleFor(command => command.IdProperty).GreaterThan(0).WithMessage("Invalid property ID.");
        RuleFor(command => command.File).NotEmpty().WithMessage("File is required.");
        RuleFor(command => command.Enabled).NotNull().WithMessage("Enabled flag is required.");
    }
}
