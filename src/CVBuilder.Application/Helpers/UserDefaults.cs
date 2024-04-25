using CVBuilder.Application.Caching;

namespace CVBuilder.Application.Helpers;

internal static class UserDefaults
{
    public static CacheKey UserByIdPrefixCacheKey => new("CVBuilder.user-by-id-{0}");
    public static CacheKey UserByAccessTokenPrefixCacheKey => new("CVBuilder.user-access-token-{0}");
}