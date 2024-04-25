using System.Linq;
using CVBuilder.Models;
using CVBuilder.Web.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CVBuilder.Web.Infrastructure.Extensions;

public static class PolicyExtensions
{
    public static void AddPolicyJwtRole(this AuthorizationOptions options, string name, RoleTypes role)
    {
        options.AddPolicyRole(name, JwtBearerDefaults.AuthenticationScheme, role);
    }

    public static void AddPolicyJwtAnyRole(this AuthorizationOptions options, string name,
        params RoleTypes[] roles)
    {
        options.AddPolicy(name, x =>
        {
            x.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            x.RequireAssertion(p => roles.Any(role => p.User.IsInRole(role.ToString())));
        });
    }

    public static void AddPolicyAccessTokenRole(this AuthorizationOptions options, string name,
        RoleTypes role)
    {
        options.AddPolicyRole(name, AccessTokenDefaults.AuthenticationScheme, role);
    }

    private static void AddPolicyRole(this AuthorizationOptions options, string name, string scheme,
        RoleTypes role)
    {
        options.AddPolicy(name, x =>
        {
            x.AuthenticationSchemes.Add(scheme);
            x.RequireRole(role.ToString());
        });
    }
}