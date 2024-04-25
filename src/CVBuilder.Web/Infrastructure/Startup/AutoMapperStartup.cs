using CVBuilder.Application;
using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Web.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure.Startup;

public class AutoMapperStartup : ICVBuilderStartup
{
    public int Order => 300; // Authorization should be loaded before Endpoint and after authentication

    /// <summary>
    ///     Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(
            typeof(MapperBase),
            typeof(AppMapperBase));
    }

    /// <summary>
    ///     Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
    }
}