using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Position.Commands;
using CVBuilder.Application.Position.Queries;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Position;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class PositionController : BaseApiController
{
    /// <summary>
    /// Create a new Position
    /// </summary>
    [HttpPost(ApiRoutes.Position.CreatePosition)]
    public async Task<ActionResult<PositionResult>> Create([FromBody] CreatePositionRequest request)
    {
        var command = Mapper.Map<CreatePositionCommand>(request);
        var result = await Mediator.Send(command);

        return Ok(result);
    }


    /// <summary>
    /// Get list of Positions
    /// </summary>
    [HttpGet(ApiRoutes.Position.GetAllPositions)]
    public async Task<ActionResult<List<PositionResult>>> GetAllPositions()
    {
        var command = new GetAllPositionQuery();
        var result = await Mediator.Send(command);
        return Ok(result);
    }


    /// <summary>
    /// Updates an existing Position
    /// </summary>
    [HttpPut(ApiRoutes.Position.UpdatePosition)]
    public async Task<ActionResult<PositionResult>> UpdatePosition(UpdatePositionRequest request)
    {
        var command = Mapper.Map<UpdatePositionCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Deleting an existing Position
    /// </summary>
    [HttpDelete(ApiRoutes.Position.DeletePosition)]
    public async Task<ActionResult> DeletePosition([FromRoute] int id)
    {
        var command = new DeletePositionCommand
        {
            PositionId = id
        };
        var result = await Mediator.Send(command);
        return Ok();
    }
}