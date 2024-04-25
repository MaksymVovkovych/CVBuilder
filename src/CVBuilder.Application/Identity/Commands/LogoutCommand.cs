using MediatR;

namespace CVBuilder.Application.Identity.Commands;

public class LogoutCommand : IRequest<bool>
{
    public int UserId { get; set; }
    public string RefreshToken { get; set; }
}