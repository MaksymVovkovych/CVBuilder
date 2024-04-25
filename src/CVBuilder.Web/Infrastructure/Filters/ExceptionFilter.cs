using System.Threading.Tasks;
using CVBuilder.Models.Exceptions;
using CVBuilder.Web.Infrastructure.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CVBuilder.Web.Infrastructure.Filters;

public class ExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;
        int? statusCode = exception switch
        {
            ForbiddenException => 403,
            ValidationException => 400,
            NotFoundException => 404,
            ConflictException => 409,
            AppException => 500,
            _ => null
        };

        if (statusCode != null)
        {
            context.Result = new JsonResult(new ApiException
            {
                Message = exception.Message,
                StatusCode = statusCode.GetValueOrDefault()
            })
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }

        return Task.CompletedTask;
    }
}