using System.Collections.Generic;
using System.Linq;
using CVBuilder.Application.Caching.Interfaces;

namespace CVBuilder.Application.Caching.Extensions;

public static class CacheExtensions
{
    public static T ToCachedFirstOrDefault<T>(this IQueryable<T> query, IStaticCacheManager staticCache,
        CacheKey cacheKey)
    {
        return cacheKey == null
            ? query.FirstOrDefault()
            : staticCache.Get(cacheKey, query.FirstOrDefault);
    }

    public static T ToCachedFirstOrDefault<T>(this List<T> query, IStaticCacheManager staticCache,
        CacheKey cacheKey)
    {
        return cacheKey == null
            ? query.FirstOrDefault()
            : staticCache.Get(cacheKey, query.FirstOrDefault);
    }
}