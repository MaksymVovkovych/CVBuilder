using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Data.Commands;
using CVBuilder.Application.Data.Queries;
using CVBuilder.Application.Data.Responses;
using CVBuilder.Models;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace CVBuilder.Web.Controllers.V1;

public class DataController : BaseApiController
{
    private readonly IWebHostEnvironment _environment;

    public DataController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }


    /// <summary>
    /// Upload image
    /// </summary>
    [HttpPost(ApiRoutes.Data.UploadImage)]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        var command = new UploadImageCommand
        {
            File = new FileData
            {
                FileName = image.FileName,
                ContentType = image.ContentType,
                FileStream = image.OpenReadStream()
            }
        };

        var response = await Mediator.Send(command);
        if (_environment.IsProduction())
            response.Url = "https://cv.ithoot.com/api/files/" + response.Path;
        return Ok(response);
    }


    /// <summary>
    /// Get list of LevelLanguage
    /// </summary>
    [HttpGet(ApiRoutes.Data.LevelLanguage)]
    public async Task<ActionResult<IEnumerable<DataTypeResult>>> GetAllLanguageLevels()
    {
        return Ok(await GetDataTypes<LanguageLevel>());
    }

    /// <summary>
    /// Get list of LevelSkills
    /// </summary>
    [HttpGet(ApiRoutes.Data.LevelSkill)]
    public async Task<ActionResult<IEnumerable<DataTypeResult>>> DriverStatuses()
    {
        return Ok(await GetDataTypes<SkillLevel>());
    }


    private async Task<IEnumerable<DataTypeResult>> GetDataTypes<TEnum>()
        where TEnum : struct, Enum
    {
        var query = new GetDataTypesQuery { EnumType = typeof(TEnum) };

        var response = await Mediator.Send(query);

        return response;
    }
}