using System.Text;
using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure.Startup;

public class AuthenticationStartup : ICVBuilderStartup
{
    /// <summary>
    ///     Gets order of this startup configuration implementation
    /// </summary>
    public int Order => 500;

    /// <summary>
    ///     Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureIdentity();

        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);

        services.ConfigureJwtAuthentication(Encoding.ASCII.GetBytes(jwtSettings.SecretKey));
    }

    /// <summary>
    ///     Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
        application.UseAuthentication();
    }
}