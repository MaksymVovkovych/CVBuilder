using System.Security.Claims;
using CVBuilder.Models.Entities;

namespace CVBuilder.Identity.Services.Interfaces;

public interface ITokenService
{
    (string, string) GenerateAccessToken(IEnumerable<Claim> claims);
    Task<string> GenerateRefreshTokenAsync(int userId, string tokenId);
    
    Task RevokeRefreshTokensAsync(int userId);
}