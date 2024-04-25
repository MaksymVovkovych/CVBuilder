using AutoMapper;
using CVBuilder.Identity.Services.Interfaces;

namespace CVBuilder.Identity.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IMapper mapper, ILogger<AuthService> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    public Task GoogleLogin()
    {
        throw new NotImplementedException();
    }

    public Task WebLogin()
    {
        throw new NotImplementedException();
    }

    public Task LoginShortByUrl()
    {
        throw new NotImplementedException();
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }

    public Task Register()
    {
        throw new NotImplementedException();
    }
}