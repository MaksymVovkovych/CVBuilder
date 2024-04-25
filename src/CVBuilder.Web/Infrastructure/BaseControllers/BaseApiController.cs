using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure.BaseControllers;

[ApiController]
public class BaseApiController : ControllerBase
{
    protected IMapper Mapper => HttpContext.RequestServices.GetRequiredService<IMapper>();

    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    //protected ILogger Logger => HttpContext.RequestServices.GetRequiredService<ILogger>();

    protected IEnumerable<string> GetModelStateErrors()
    {
        return ModelState.Values
            .SelectMany(r => r.Errors
                .Select(b => b.ErrorMessage));
    }
}