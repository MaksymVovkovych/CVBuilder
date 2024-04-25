using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using CVBuilder.Web.Infrastructure.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CVBuilder.Web.Infrastructure.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder application)
    {
        application.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    ApiException result;

                    if (contextFeature.Error is ValidationException validationException)
                        result = new ApiException
                        {
                            StatusCode = (int) HttpStatusCode.BadRequest,
                            Message = "Validation error.",
                            Errors = validationException.Errors.Select(r => r.ToString())
                        };
                    else
                        result = new ApiException
                        {
                            StatusCode = (int) HttpStatusCode.InternalServerError,
                            Message = "Internal Server error.",
                            Errors = new List<string> {contextFeature.Error.Message}
                        };

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = result.StatusCode;

                    var text = JsonSerializer.Serialize(result);

                    await context.Response.WriteAsync(text);
                }
            });
        });
    }
}