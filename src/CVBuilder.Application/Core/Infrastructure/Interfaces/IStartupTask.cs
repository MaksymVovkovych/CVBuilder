using System.Threading.Tasks;

namespace CVBuilder.Application.Core.Infrastructure.Interfaces;

/// <summary>
///     Interface which should be implemented by tasks run on startup
/// </summary>
public interface IStartupTask
{
    /// <summary>
    ///     Gets order of this startup task implementation
    /// </summary>
    int Order { get; }

    /// <summary>
    ///     Executes a task
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task ExecuteAsync();
}