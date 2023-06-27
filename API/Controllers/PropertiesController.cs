using Application.Common.Models;
using Application.Dto;
using Application.Property.Commands.CreateProperty;
using Application.Property.Commands.UpdateProperty;
using Application.Property.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class PropertiesController : ApiControllerBase
{

    /// <summary>
    /// Get a list of properties.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<PaginatedList<PropertyDto>>> GetProperties([FromQuery] GetPropertiesQuery query)
    {
        return await Mediator.Send(query);
    }

    /// <summary>
    /// Method for creating a new property
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<PropertyDto>> CreateProperty(CreatePropertyCommand command)
    {
        return await Mediator.Send(command);
    }

    /// <summary>
    /// Method for updating a property
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<string>> UpdateProperty(int id, UpdatePropertyCommand command)
    {
        if (id != command.IdProperty)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }
}
