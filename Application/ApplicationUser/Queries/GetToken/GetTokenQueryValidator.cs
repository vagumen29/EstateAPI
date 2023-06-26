using FluentValidation;

namespace Application.ApplicationUser.Queries.GetToken;

public class GetTokenQueryValidator: AbstractValidator<GetTokenQuery>
{
    public GetTokenQueryValidator()
    {
        RuleFor(query => query.Email).NotEmpty().WithMessage("Email is required.")
                                     .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(query => query.Password).NotEmpty().WithMessage("Password is required.");
                                       
    }
}
