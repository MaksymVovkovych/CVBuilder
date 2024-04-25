namespace CVBuilder.Application.Caching;

public static class CachingDefaults
{
    /// <summary>
    ///     Gets the default cache time in minutes
    /// </summary>
    public static int CacheTime => 60;

    public static int ShortTermCacheTime => 5;

    /// <summary>
    ///     Gets a key for caching
    /// </summary>
    /// <remarks>
    ///     {0} : Entity type name
    ///     {1} : Entity id
    /// </remarks>
    public static string EntityCacheKey => "ITHoot.{0}.id-{1}";
}