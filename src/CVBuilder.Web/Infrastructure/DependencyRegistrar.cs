using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Application.Core.Settings;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure;

public class DependencyRegistrar : IDependencyRegistrar
{
    public int Order => 0;

    /// <summary>
    ///     Register services and interfaces
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="typeFinder">Type finder</param>
    /// <param name="appSettings">App settings</param>
    public virtual void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
    {
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
    }
}