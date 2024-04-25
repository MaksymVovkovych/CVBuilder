using System.Security.Claims;
using AutoMapper;
using CVBuilder.Identity.Services.Interfaces;
using CVBuilder.Models.Entities;

namespace CVBuilder.Identity.Services;

public class TokenService : ITokenService
{
    private readonly IMapper _mapper;
    private readonly ILogger<TokenService> _logger;
    
    public TokenService(IMapper mapper, ILogger<TokenService> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    public (string, string) GenerateAccessToken(IEnumerable<Claim> claims)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateRefreshTokenAsync(int userId, string tokenId)
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        throw new NotImplementedException();
    }

    public Task<RefreshToken> GetRefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<RefreshToken> GetRefreshTokenAsync(int userId, string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRefreshTokenAsync(RefreshToken storedRefreshToken)
    {
        throw new NotImplementedException();
    }

    public Task RevokeRefreshTokenAsync(int userId, string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task RevokeRefreshTokensAsync(int userId)
    {
        throw new NotImplementedException();
    }
}