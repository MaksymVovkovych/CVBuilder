using CVBuilder.Application.Core.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Application.Core.Infrastructure.Interfaces;

/// <summary>
///     Dependency registrar interface
/// </summary>
public interface IDependencyRegistrar
{
    /// <summary>
    ///     Gets order of this dependency registrar implementation
    /// </summary>
    int Order { get; }

    /// <summary>
    ///     Register services and interfaces
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="typeFinder">Type finder</param>
    void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings);
}