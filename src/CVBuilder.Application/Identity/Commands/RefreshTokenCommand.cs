using CVBuilder.Application.Identity.Responses;
using MediatR;

namespace CVBuilder.Application.Identity.Commands;

public class RefreshTokenCommand : IRequest<AuthenticationResult>
{
    // public string Token { get; set; }
    public string RefreshToken { get; set; }
    // public bool ForceRefresh { get; set; }
}