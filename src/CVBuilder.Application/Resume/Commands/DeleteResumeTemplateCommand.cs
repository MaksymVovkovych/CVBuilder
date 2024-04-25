using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class DeleteResumeTemplateCommand : IRequest<bool>
{
    public int Id { get; set; }
}