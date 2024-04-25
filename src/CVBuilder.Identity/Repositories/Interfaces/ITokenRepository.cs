using CVBuilder.Models.Entities;

namespace CVBuilder.Identity.Repositories.Interfaces;

public interface ITokenRepository
{
    Task<AccessToken> GenerateAccessToken(User user);
    Task<string> GenerateRefreshToken();
    Task RevokeRefreshToken(string refreshToken, int userId);
}