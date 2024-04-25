using CVBuilder.Application.Core.Infrastructure;
using CVBuilder.Application.Core.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CVBuilder.Web.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureRequestPipeline(this IApplicationBuilder application)
    {
        EngineContext.Current.ConfigureRequestPipeline(application);
    }

    public static void UseSVBuilderSwagger(this IApplicationBuilder application)
    {
        var swaggerSettings = application.ApplicationServices.GetService<IOptions<SwaggerSettings>>().Value;

        application.UseSwagger(options => options.RouteTemplate = swaggerSettings.JsonRoute);

        application.UseSwaggerUI(options =>
        {
            options.DocExpansion(DocExpansion.None);
            options.SwaggerEndpoint(swaggerSettings.UIEndpoint, swaggerSettings.Description);
        });
    }
}