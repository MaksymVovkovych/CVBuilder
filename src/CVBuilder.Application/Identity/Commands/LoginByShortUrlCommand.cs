using CVBuilder.Application.Identity.Responses;
using MediatR;

namespace CVBuilder.Application.Identity.Commands;

public class LoginByShortUrlCommand : IRequest<AuthenticationResult>
{
    public string ShortUrl { get; set; }
}