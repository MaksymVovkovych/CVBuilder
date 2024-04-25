using System;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web.Infrastructure.Extensions;

public static class CustomCacheExtension
{
    public static IServiceCollection UseEfCoreCache(this IServiceCollection services)
    {
        services.AddEFSecondLevelCache(opt =>
        {
            var time = TimeSpan.FromDays(1);
            opt.UseMemoryCacheProvider(CacheExpirationMode.Absolute, time)
        
                .DisableLogging(false)
                .CacheAllQueries(CacheExpirationMode.Absolute, time)
                .UseCacheKeyPrefix("EF_");
        });

        return services;
    }
}