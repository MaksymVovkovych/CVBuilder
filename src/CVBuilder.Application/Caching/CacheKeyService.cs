﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CVBuilder.Application.Caching.Interfaces;
using CVBuilder.Application.Helpers;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Application.Caching;

public class CacheKeyService : ICacheKeyService
{
    private const string HASH_ALGORITHM = "SHA1";

    /// <summary>
    ///     Creates a copy of cache key and fills it by set parameters
    /// </summary>
    /// <param name="cacheKey">Initial cache key</param>
    /// <param name="keyObjects">Parameters to create cache key</param>
    /// <returns>Cache key</returns>
    public virtual CacheKey PrepareKey(CacheKey cacheKey, params object[] keyObjects)
    {
        return FillCacheKey(cacheKey, keyObjects);
    }

    /// <summary>
    ///     Creates a copy of cache key using the default cache time and fills it by set parameters
    /// </summary>
    /// <param name="cacheKey">Initial cache key</param>
    /// <param name="keyObjects">Parameters to create cache key</param>
    /// <returns>Cache key</returns>
    public virtual CacheKey PrepareKeyForDefaultCache(CacheKey cacheKey, params object[] keyObjects)
    {
        var key = FillCacheKey(cacheKey, keyObjects);

        key.CacheTime = CachingDefaults.CacheTime;

        return key;
    }

    /// <summary>
    ///     Creates a copy of cache key using the short cache time and fills it by set parameters
    /// </summary>
    /// <param name="cacheKey">Initial cache key</param>
    /// <param name="keyObjects">Parameters to create cache key</param>
    /// <returns>Cache key</returns>
    public virtual CacheKey PrepareKeyForShortTermCache(CacheKey cacheKey, params object[] keyObjects)
    {
        var key = FillCacheKey(cacheKey, keyObjects);

        key.CacheTime = CachingDefaults.ShortTermCacheTime;

        return key;
    }

    /// <summary>
    ///     Creates the cache key prefix
    /// </summary>
    /// <param name="keyFormatter">Key prefix formatter string</param>
    /// <param name="keyObjects">Parameters to create cache key prefix</param>
    /// <returns>Cache key prefix</returns>
    public virtual string PrepareKeyPrefix(string keyFormatter, params object[] keyObjects)
    {
        return keyObjects?.Any() ?? false
            ? string.Format(keyFormatter, keyObjects.Select(CreateCacheKeyParameters).ToArray())
            : keyFormatter;
    }

    /// <summary>
    ///     Creates the hash sum of identifiers list
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    protected virtual string CreateIdsHash(IEnumerable<int> ids)
    {
        var identifiers = ids.ToList();

        if (!identifiers.Any())
            return string.Empty;

        return HashHelper.CreateHash(
            Encoding.UTF8.GetBytes(string.Join(", ", identifiers.OrderBy(id => id))),
            HASH_ALGORITHM);
    }

    /// <summary>
    ///     Converts an object to cache parameter
    /// </summary>
    /// <param name="parameter">Object to convert</param>
    /// <returns>Cache parameter</returns>
    protected virtual object CreateCacheKeyParameters(object parameter)
    {
        return parameter switch
        {
            null => "null",
            IEnumerable<int> ids => CreateIdsHash(ids),
            IEnumerable<IEntity<int>> entities => CreateIdsHash(entities.Select(e => e.Id)),
            IEntity<int> entity => entity.Id,
            decimal param => param.ToString(CultureInfo.InvariantCulture),
            _ => parameter
        };
    }

    /// <summary>
    ///     Creates a copy of cache key and fills it by set parameters
    /// </summary>
    /// <param name="cacheKey">Initial cache key</param>
    /// <param name="keyObjects">Parameters to create cache key</param>
    /// <returns>Cache key</returns>
    protected virtual CacheKey FillCacheKey(CacheKey cacheKey, params object[] keyObjects)
    {
        return new CacheKey(cacheKey, CreateCacheKeyParameters, keyObjects);
    }
}