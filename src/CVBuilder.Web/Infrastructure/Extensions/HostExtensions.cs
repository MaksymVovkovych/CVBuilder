

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CVBuilder.EFContext;

namespace CVBuilder.Web.Infrastructure.Extensions;

public static class HostExtensions
{
    public static async Task<IHost> EnsureDbExistsAsync(this IHost host)
    {
         using var scope = host.Services.CreateScope();

        //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "EncryptMigrate")
         //   await host.EncryptData();

        
        var services = scope.ServiceProvider;
         await using var context = services.GetService<IdentityEfDbContext>();

         await context.Database.EnsureCreatedAsync();
         //await context.Database.MigrateAsync();
         //await DbInitializer.Initialize(context);

        return host;
    }
}