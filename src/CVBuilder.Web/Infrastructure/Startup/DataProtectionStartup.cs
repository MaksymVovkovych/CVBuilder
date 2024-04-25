using CVBuilder.Application.Core.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure.Startup;

public class DataProtectionStartup : ICVBuilderStartup
{
    /// <summary>
    ///     Gets order of this startup configuration implementation
    /// </summary>
    public int Order => 700;

    /// <summary>
    ///     Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //services.AddScoped<ILookupProtectorKeyRing, KeyRing>();
        //services.AddScoped<ILookupProtector, LookupProtector>();
    }

    /// <summary>
    ///     Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
    }
}