using CVBuilder.Models.Entities;

namespace CVBuilder.Identity.Repositories.Interfaces;

public interface IAuthRepository
{
    Task<AccessToken> Register(User user);
    Task<AccessToken> Login(User user);
    Task Logout();
}