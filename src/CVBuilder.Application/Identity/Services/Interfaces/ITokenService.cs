using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Identity.Services.Interfaces;

public interface ITokenService
{
    (string, string) GenerateAccessToken(IEnumerable<Claim> claims);
    Task<string> GenerateRefreshTokenAsync(int userId, string tokenId);
    ClaimsPrincipal GetPrincipalFromToken(string token);
    Task<RefreshToken> GetRefreshTokenAsync(string refreshToken);
    Task<RefreshToken> GetRefreshTokenAsync(int userId, string refreshToken);
    Task UpdateRefreshTokenAsync(RefreshToken storedRefreshToken);
    Task RevokeRefreshTokenAsync(int userId, string refreshToken);
    Task RevokeRefreshTokensAsync(int userId);
}