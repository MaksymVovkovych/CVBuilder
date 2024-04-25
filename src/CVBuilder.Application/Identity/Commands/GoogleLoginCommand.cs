using CVBuilder.Application.Identity.Responses;
using MediatR;

namespace CVBuilder.Application.Identity.Commands;

public class GoogleLoginCommand : IRequest<AuthenticationResult>
{
    public string IdToken { get; set; }
}