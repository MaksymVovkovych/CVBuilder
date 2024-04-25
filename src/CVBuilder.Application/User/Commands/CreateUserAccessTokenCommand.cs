using CVBuilder.Application.User.Responses;
using MediatR;

namespace CVBuilder.Application.User.Commands;

public class CreateUserAccessTokenCommand : IRequest<UserAccessTokenResult>
{
    public int UserId { get; set; }
    public int? ExpireDays { get; set; }
}