using Application.ApplicationUser.Queries.GetToken;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class LoginController : ApiControllerBase
{
    /// <summary>
    /// Method for login user.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<LoginDto>> Login(GetTokenQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }
}

