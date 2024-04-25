using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Models;
using CVBuilder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure.Startup;

public class AuthorizationStartup : ICVBuilderStartup
{
    public int Order => 600; // Authorization should be loaded before Endpoint and after authentication

    /// <summary>
    ///     Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicyJwtRole(Constants.Policy.Admin, RoleTypes.Admin);
            options.AddPolicyJwtRole(Constants.Policy.User, RoleTypes.User);
            options.AddPolicyJwtRole(Constants.Policy.HR, RoleTypes.Hr);
        });

        //services.AddScoped<IAuthorizationHandler, PolicyAuthorizationHandler>();
    }

    /// <summary>
    ///     Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
        application.UseAuthorization();
    }
}