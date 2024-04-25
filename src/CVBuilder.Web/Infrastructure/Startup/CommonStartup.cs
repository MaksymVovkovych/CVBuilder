using AngleSharp.Io;
using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure.Startup;

public class CommonStartup : ICVBuilderStartup
{
    /// <summary>
    ///     Gets order of this startup configuration implementation
    /// </summary>
    public int Order => 100; //common services should be loaded after error handlers

    /// <summary>
    ///     Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //services.AddResponseCompression();

        services.AddOptions();

        services.AddCVBuilderDistributedCache();

        services.AddHttpSession();

        services.AddLogging();

        services.AddRouting();
    }

    /// <summary>
    ///     Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
        application.UseStaticFiles(new StaticFileOptions()
        {
            ServeUnknownFileTypes = true,
            OnPrepareResponse = ctx =>
            {
                const int durationInSeconds = 60 * 60;
                ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                    "public,max-age=" + durationInSeconds;
            }

        });

        application.UseSession();
    }
}