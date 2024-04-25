using CVBuilder.Application.User.Responses;
using MediatR;

namespace CVBuilder.Application.User.Commands;

public class CreateUserCommand : IRequest<UserResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}