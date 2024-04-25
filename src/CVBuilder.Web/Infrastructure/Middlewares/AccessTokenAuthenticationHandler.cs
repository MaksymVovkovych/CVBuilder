using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CVBuilder.Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CVBuilder.Web.Infrastructure.Middlewares;

public class AccessTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IMediator _mediator;

    public AccessTokenAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IMediator mediator)
        : base(options, logger, encoder, clock)
    {
        _mediator = mediator;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization")) return AuthenticateResult.Fail("Unauthorized");

        var authHeaderValue = Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authHeaderValue)) return AuthenticateResult.NoResult();

        if (!authHeaderValue
                .StartsWith(AccessTokenDefaults.AuthenticationScheme, StringComparison.InvariantCultureIgnoreCase))
            return AuthenticateResult.Fail("Unauthorized");

        var token = authHeaderValue[AccessTokenDefaults.AuthenticationScheme.Length..].Trim();
        if (string.IsNullOrEmpty(token)) return AuthenticateResult.Fail("Unauthorized");

        try
        {
            var result = await _mediator.Send(new GetUserClaimsByAccessTokenQuery(token));
            if (result.IsTokenExpired) return AuthenticateResult.Fail("Ticket expired");

            var identity = new ClaimsIdentity(result.Claims, AccessTokenDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(),
                AccessTokenDefaults.AuthenticationScheme);

            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex.Message);
        }
    }
}