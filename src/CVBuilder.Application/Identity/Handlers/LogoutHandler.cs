using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.Identity.Services.Interfaces;
using MediatR;

namespace CVBuilder.Application.Identity.Handlers;

public class LogoutHandler : IRequestHandler<LogoutCommand, bool>
{
    private readonly ITokenService _tokenService;

    public LogoutHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _tokenService.RevokeRefreshTokenAsync(request.UserId, request.RefreshToken);
        return true;
    }
}