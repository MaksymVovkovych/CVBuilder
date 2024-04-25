using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.Identity.Responses;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Models.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Identity.Handlers;

public class LoginByShortUrlHandler : IRequestHandler<LoginByShortUrlCommand, AuthenticationResult>
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<Models.Entities.User> _manager;

    public LoginByShortUrlHandler(UserManager<Models.Entities.User> manager, IIdentityService identityService)
    {
        _manager = manager;
        _identityService = identityService;
    }

    public async Task<AuthenticationResult> Handle(LoginByShortUrlCommand request, CancellationToken cancellationToken)
    {
        var user = await _manager.Users
            .Include(x => x.ShortUrl)
            .FirstOrDefaultAsync(x => x.ShortUrl.Url == request.ShortUrl,
                cancellationToken);

        if (user == null) throw new ForbiddenException("User does not exist");

        if (user.ShortUrl == null) throw new ForbiddenException("Wrong url");

        return await _identityService.GenerateAuthenticationResultAsync(user);
    }
}