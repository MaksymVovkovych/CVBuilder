using CVBuilder.EFContext;
using CVBuilder.Identity.Repositories.Interfaces;
using CVBuilder.Models.Entities;

namespace CVBuilder.Identity.Repositories;

public class TokenRepository :  ITokenRepository
{
    private readonly EfDbContext _efContext;

    public TokenRepository(EfDbContext efContext)
    {
        _efContext = efContext;
    }

    public Task<AccessToken> GenerateAccessToken(User user)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public Task RevokeRefreshToken(string refreshToken, int userId)
    {
        throw new NotImplementedException();
    }
}