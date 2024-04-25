using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class DeleteResumeCommand : IRequest<bool>
{
    public int Id { get; set; }
    public int UserId { get; set; }
}