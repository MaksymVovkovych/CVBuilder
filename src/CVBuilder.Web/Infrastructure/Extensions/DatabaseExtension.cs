using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure.Extensions;

public static class DatabaseExtension
{
    public static IServiceCollection AddDataBaseContext( this IServiceCollection services, IConfiguration configuration)
    {
        // var connectionString = configuration.GetConnectionString("DefaultConnection");
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        //
        //
        // services.AddDbContext<EfDbContext>((serviceProvider, options) =>
        //     options.UseNpgsql(connectionString,
        //             opt => 
        //                  opt.MigrationsAssembly(typeof(EfDbContext).Assembly.GetName().Name)
        //                 .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
        //         .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>())
        // );
        
        // services.AddDbContext<IdentityEfDbContext>((serviceProvider, options) =>
        //     options.UseNpgsql(connectionString,
        //             opt => opt.MigrationsAssembly(typeof(IdentityEfDbContext).Assembly.GetName().Name)
        //                 .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
        //         .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>())
        // );

        return services;
    }
}
