using System;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.Identity.Responses;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using CVBuilder.Models.Exceptions;
using MediatR;

namespace CVBuilder.Application.Identity.Handlers;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthenticationResult>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;
    private readonly IAppUserManager _userManager;

    public RefreshTokenHandler(
        IAppUserManager userManager,
        IIdentityService identityService,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _identityService = identityService;
        _tokenService = tokenService;
    }

    public async Task<AuthenticationResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // var validatedToken = _tokenService.GetPrincipalFromToken(request.Token);
        // if (validatedToken == null)
        // {
        //     return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
        // }

        // var expiryDate = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value;
        // var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        //     .AddSeconds(long.Parse(expiryDate));
        // if (expiryDateTimeUtc > DateTime.UtcNow && !request.ForceRefresh)
        // {
        //     return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
        // }

        // var userId = validatedToken.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var storedRefreshToken = await _tokenService.GetRefreshTokenAsync(request.RefreshToken);
        if (storedRefreshToken == null) throw new ForbiddenException("This refresh token does not exist");

        if (DateTime.UtcNow > storedRefreshToken.ExpiryAt)
            throw new ForbiddenException("This refresh token has expired");

        if (storedRefreshToken.IsRevoked) throw new ForbiddenException("This refresh token has been revoked");

        if (storedRefreshToken.IsUsed) throw new ForbiddenException("This refresh token has been used");

        // var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
        // if (storedRefreshToken.JwtId != jti)
        // {
        //     return new AuthenticationResult { Errors = new[] { "This refresh token does not match this JWT" } };
        // }
        var userId = storedRefreshToken.UserId;
        storedRefreshToken.IsUsed = true;
        await _tokenService.UpdateRefreshTokenAsync(storedRefreshToken);
        var user = await _userManager.FindByIdAsync(userId);
        return await _identityService.GenerateAuthenticationResultAsync(user);
    }
}