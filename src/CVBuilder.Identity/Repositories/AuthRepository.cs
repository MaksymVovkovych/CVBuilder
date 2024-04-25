using CVBuilder.EFContext;
using CVBuilder.Identity.Repositories.Interfaces;
using CVBuilder.Models.Entities;

namespace CVBuilder.Identity.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly EfDbContext _efContext;

    public AuthRepository(EfDbContext efContext)
    {
        _efContext = efContext;
    }

    public Task<AccessToken> Register(User user)
    {
        throw new NotImplementedException();
    }

    public Task<AccessToken> Login(User user)
    {
        throw new NotImplementedException();
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }
}