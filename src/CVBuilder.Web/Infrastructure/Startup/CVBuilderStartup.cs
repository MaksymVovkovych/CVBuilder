using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure.Startup;

/// <summary>
///     Represents object for the configuring MVC on application startup
/// </summary>
public class CVBuilderStartup : ICVBuilderStartup
{
    public int Order => 1000; //MVC should be loaded last

    /// <summary>
    ///     Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // services.AddMemoryCache();
        services.AddControllers(options => { options.Filters.Add(new ExceptionFilter()); });
    }

    /// <summary>
    ///     Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
        application.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            //todo
            //endpoints.MapHub<DriverHub>(typeof(DriverHub).GetCustomAttribute<SignalRHubAttribute>()?.Path);
        });
    }
}