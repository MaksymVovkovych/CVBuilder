using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Experience.Commands;
using CVBuilder.Application.Experience.Queries;
using CVBuilder.Application.Experience.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Experiance;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class ExperienceController : BaseApiController
{
    /// <summary>
    /// Create a new Experience
    /// </summary>
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Experience.CreateExperience)]
    public async Task<ActionResult<CreateExperienceResult>> Create(CreateExperiance query)
    {
        var command = Mapper.Map<CreateExperienceCommand>(query);
        var response = await Mediator.Send(command);

        return Ok(response);
    }

    /// <summary>
    /// Get list of Experiences
    /// </summary>
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Experience.GetAllExperience)]
    public async Task<ActionResult<IEnumerable<CreateExperienceResult>>> GetAllExperiences(
        [FromQuery] GetAllExperiances query)
    {
        var command = Mapper.Map<GetAllExperiencesQuery>(query);
        var response = await Mediator.Send(command);

        return Ok(response);
    }

    /// <summary>
    /// Get Experience by ID
    /// </summary>
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Experience.GetExperienceById)]
    public async Task<ActionResult<GetExperienceByIdResult>> GetExperience(int id)
    {
        var command = new GetExperienceByIdQuery
        {
            Id = id
        };
        var response = await Mediator.Send(command);
        return Ok(response);
    }
}