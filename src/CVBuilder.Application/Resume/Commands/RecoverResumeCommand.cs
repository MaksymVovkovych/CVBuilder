using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class RecoverResumeCommand : IRequest<ResumeCardResult>
{
    public int Id { get; set; }
    public int UserId { get; set; }
}