using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Education.Commands;
using CVBuilder.Application.Education.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Education;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EducationResult = CVBuilder.Application.Resume.Responses.Shared.EducationResult;

namespace CVBuilder.Web.Controllers.V1;

public class EducationController : BaseApiController
{
    /// <summary>
    /// Create a new Education
    /// </summary>
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Education.CreateEducation)]
    public async Task<ActionResult<CreateEducationResult>> Create(CreateEducation query)
    {
        var command = Mapper.Map<CreateEducationCommand>(query);
        var response = await Mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Get list of Educations
    /// </summary>
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Education.GetAllEducation)]
    public async Task<ActionResult<IEnumerable<EducationResult>>> GetAllEducations(
        [FromQuery] GetAllEducation query)
    {
        var command = Mapper.Map<GetAllEducationsCommand>(query);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get Education by ID
    /// </summary>
    [AllowAnonymous]
    [HttpGet(ApiRoutes.Education.GetEducation)]
    public async Task<ActionResult<EducationByIdResult>> GetEducations(int id)
    {
        var command = new GetEducationByIdCommand
        {
            Id = id
        };

        var response = await Mediator.Send(command);
        return Ok(response);
    }
}