using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Language.Commands;
using CVBuilder.Application.Language.Queries;
using CVBuilder.Application.Language.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Language;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class LanguageController : BaseApiController
{
    /// <summary>
    /// Create a new Language
    /// </summary>
    [HttpPost(ApiRoutes.Language.CreateLanguage)]
    public async Task<ActionResult<LanguageResult>> Create([FromBody] CreateLanguageRequest query)
    {
        var command = Mapper.Map<CreateLanguageCommand>(query);
        var result = await Mediator.Send(command);
        return Ok(result);
    }


    /// <summary>
    /// Get list of Languages
    /// </summary>
    [HttpGet(ApiRoutes.Language.LanguageGetAll)]
    public async Task<ActionResult<LanguageResult>> GetAllLanguage()
    {
        var query = new GetAllLanguagesRequest();
        var command = Mapper.Map<GetAllLanguagesQuery>(query);
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing Language
    /// </summary>
    [HttpPut(ApiRoutes.Language.UpdateLanguage)]
    public async Task<ActionResult<LanguageResult>> UpdateLanguage(UpdateLanguageRequest request)
    {
        var command = Mapper.Map<UpdateLanguageCommand>(request);
        var result = await Mediator.Send(command);
        return Ok(result);
    }


    /// <summary>
    /// Deleting an existing Language
    /// </summary>
    [HttpDelete(ApiRoutes.Language.DeleteLanguage)]
    public async Task<ActionResult> DeleteSkill([FromRoute] int id)
    {
        var command = new DeleteLanguageCommand
        {
            Id = id
        };
        var result = await Mediator.Send(command);
        return Ok();
    }

    /// <summary>
    /// Get Language by content Text
    /// </summary>
    [HttpGet(ApiRoutes.Language.GetLanguage)]
    public async Task<ActionResult<IEnumerable<LanguageResult>>> GetLanguage(
        [FromQuery] GetLanguagesByContentText query)
    {
        var command = Mapper.Map<GetLanguageByContainInTextQuery>(query);
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}