using CVBuilder.Application.Identity.Responses;
using MediatR;

namespace CVBuilder.Application.Identity.Commands;

public class GetCurrentUserByTokenCommand : IRequest<AuthenticationResult>
{
    public int UserId { get; set; }
}