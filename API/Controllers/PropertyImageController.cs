﻿using Application.PropertyImage.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class PropertyImageController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<string>> Create(CreatePropertyImageCommand command)
    {
        return await Mediator.Send(command);
    }
}
