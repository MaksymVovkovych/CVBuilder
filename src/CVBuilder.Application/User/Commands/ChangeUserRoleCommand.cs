using CVBuilder.Application.User.Responses;
using MediatR;

namespace CVBuilder.Application.User.Commands;

public class ChangeUserRoleCommand : IRequest<SmallUserResult>
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}