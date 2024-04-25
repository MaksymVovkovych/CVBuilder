using System;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Caching.Interfaces;
using CVBuilder.Application.User.Queries;
using CVBuilder.Application.User.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.User.Handlers;

public class
    GetUserClaimsByAccessTokenHandler : IRequestHandler<GetUserClaimsByAccessTokenQuery,
        UserAccessTokenClaimsResult>
{
    private readonly IRepository<AccessToken, int> _accessTokenRepository;
    // private readonly ICacheKeyService _cacheKeyService;
    private readonly IStaticCacheManager _staticCacheManager;

    public GetUserClaimsByAccessTokenHandler(
        IRepository<AccessToken, int> accessTokenRepository
        // ICacheKeyService cacheKeyService
        // IStaticCacheManager staticCacheManager
        )
    {
        _accessTokenRepository = accessTokenRepository;
        // _cacheKeyService = cacheKeyService;
        // _staticCacheManager = staticCacheManager;
    }

    public async Task<UserAccessTokenClaimsResult> Handle(GetUserClaimsByAccessTokenQuery request,
        CancellationToken cancellationToken)
    {
        // var key = _cacheKeyService.PrepareKeyForDefaultCache(UserDefaults.UserByAccessTokenPrefixCacheKey,
            // request.AccessToken);
        // var (expiryAt, claims) = await _staticCacheManager
        //     .GetAsync(key, async () =>
        //     {
                var item = await _accessTokenRepository.Table
                    .Include(r => r.User)
                    .SingleAsync(r => r.Token == request.AccessToken, cancellationToken);

                // return (item.ExpiryAt, await _tokenService.GetUserClaims(item.User));
            // });

        return new UserAccessTokenClaimsResult
        {
           
        };
    }
}
