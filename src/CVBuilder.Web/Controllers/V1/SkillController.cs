using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Skill.Commands;
using CVBuilder.Application.Skill.DTOs;
using CVBuilder.Application.Skill.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Skill;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class SkillController : BaseApiController
{
    /// <summary>
    /// Create a new Skill
    /// </summary>
    [HttpPost(ApiRoutes.Skill.CreateSkill)]
    public async Task<ActionResult<SkillResult>> Create([FromBody] CreateSkillRequest request)
    {
        var command = Mapper.Map<CreateSkillCommand>(request);
        var result = await Mediator.Send(command);

        return Ok(result);
    }


    /// <summary>
    /// Get list of Skills
    /// </summary>
    [HttpGet(ApiRoutes.Skill.SkillsGetAll)]
    public async Task<ActionResult<IEnumerable<SkillResult>>> GetAllSkills([FromQuery] GetAllSkillRequest request)
    {
        var command = Mapper.Map<GetAllSkillQuery>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }


    /// <summary>
    /// Updates an existing Skill
    /// </summary>
    [HttpPut(ApiRoutes.Skill.UpdateSkill)]
    public async Task<ActionResult<SkillResult>> UpdateSkill(UpdateSkillRequest request)
    {
        var command = Mapper.Map<UpdateSkillCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Deleting an existing Skill
    /// </summary>
    [HttpDelete(ApiRoutes.Skill.DeleteSkill)]
    public async Task<ActionResult> DeleteSkill([FromRoute] int id)
    {
        var command = new DeleteSkillCommand
        {
            SkillId = id
        };
        var result = await Mediator.Send(command);
        return Ok();
    }

    /// <summary>
    /// Get Skill by content Text
    /// </summary>
    [HttpGet(ApiRoutes.Skill.GetSkill)]
    public async Task<ActionResult<IEnumerable<SkillResult>>> GetSkill([FromQuery] GetSkillByContainText query)
    {
        var command = Mapper.Map<GetSkillByContainInTextQuery>(query);
        var result = await Mediator.Send(command);

        return Ok(result);
    }
}