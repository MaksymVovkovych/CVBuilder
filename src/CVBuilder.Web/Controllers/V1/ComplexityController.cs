using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Complexity.Commands;
using CVBuilder.Application.Complexity.Queries;
using CVBuilder.Application.Complexity.Result;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Complexity;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class ComplexityController : BaseAuthApiController
{
    /// <summary>
    /// Create a new Complexity
    /// </summary>
    [HttpPost(ApiRoutes.Complexity.CreateComplexity)]
    public async Task<ActionResult<ComplexityResult>> Create([FromBody] CreateComplexityRequest request)
    {
        var command = Mapper.Map<CreateComplexityCommand>(request);
        var result = await Mediator.Send(command);

        return Ok(result);
    }

    /// <summary>
    /// Updates an existing Complexity
    /// </summary>
    [HttpPut(ApiRoutes.Complexity.UpdateComplexity)]
    public async Task<ActionResult<ComplexityResult>> UpdatePosition(UpdateComplexityRequest request)
    {
        var command = Mapper.Map<UpdateComplexityCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get list of Complexities
    /// </summary>
    [HttpGet(ApiRoutes.Complexity.GetAllComplexities)]
    public async Task<ActionResult<List<ComplexityResult>>> GetAllProposals()
    {
        var command = new GetAllComplexitiesQuery();
        var result = await Mediator.Send(command);
        return result;
    }

    /// <summary>
    /// Deleting an existing Complexity
    /// </summary>
    [HttpDelete(ApiRoutes.Complexity.DeleteComplexity)]
    public async Task<ActionResult> DeletePosition([FromRoute] int id)
    {
        var command = new DeleteComplexityCommand
        {
            Id = id
        };
        var result = await Mediator.Send(command);
        return Ok();
    }
}