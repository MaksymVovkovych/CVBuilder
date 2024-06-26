﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CVBuilder.Web.Infrastructure.Middlewares;

public class ExceptionMiddleware
{
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong: {Ex}", ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;

        response.ContentType = "application/json";
        response.StatusCode = exception switch
        {
            _ => 500
        };
        return context.Response.WriteAsJsonAsync(new {message = "Something went wrong"});
    }

    #region

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    #endregion
}