using System.Threading.Tasks;
using CVBuilder.Application.Caching.Interfaces;
using CVBuilder.Application.Helpers;
using CVBuilder.Models.Entities.Interfaces;
using CVBuilder.Repository;

namespace CVBuilder.Application.Caching.Extensions;

public static class IRepositoryExtensions
{
    /// <summary>
    ///     Gets a cached element by Id
    /// </summary>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    /// <param name="repository">Entity repository</param>
    /// <param name="id">Entity identifier</param>
    /// <returns>Cached entity</returns>
    public static Task<TEntity> ToCachedGetById<TEntity>(
        this IRepository<TEntity, int> repository,
        IStaticCacheManager staticCacheManager,
        int id
    ) where TEntity : IEntity<int>
    {
        var cacheKey = new CacheKey(CommonHelper.GetEntityCacheKey(typeof(TEntity), id));

        return staticCacheManager.GetAsync(cacheKey, async () => await repository.GetByIdAsync(id));
    }
}