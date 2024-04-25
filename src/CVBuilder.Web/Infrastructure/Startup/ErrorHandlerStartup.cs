using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Web.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure.Startup;

public class ErrorHandlerStartup : ICVBuilderStartup
{
    /// <summary>
    ///     Gets order of this startup configuration implementation
    /// </summary>
    public int Order => 0; //error handlers should be loaded first

    /// <summary>
    ///     Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    /// <summary>
    ///     Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
        application.UseMiddleware<ExceptionMiddleware>();

        // application.ConfigureExceptionHandler();
    }
}