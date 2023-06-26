using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Dto;
using MediatR;

namespace Application.ApplicationUser.Queries.GetToken;

public class GetTokenQuery : IRequest<LoginDto>
{
    public string Email { get; set; }

    public string Password { get; set; }

}

public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, LoginDto>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;
    public GetTokenQueryHandler(ITokenService tokenService, IIdentityService identityService)
    {
        _identityService = identityService;
        _tokenService = tokenService;

    }
    public async Task<LoginDto> Handle(GetTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.CheckUserPassword(request.Email, request.Password);
        if (user == null)
        {
            throw new ForbiddenAccessException();
        }

        return new LoginDto
        {
            User = user,
            Token = _tokenService.CreateJwtToken(user.Id)
        };
    }
}
