using CVBuilder.Application.Identity.Responses;
using MediatR;

namespace CVBuilder.Application.Identity.Commands;

public class RegisterCommand : IRequest<AuthenticationResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
}