using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers;

public class DeleteResumeTemplateHandler : IRequestHandler<DeleteResumeTemplateCommand, bool>
{
    private readonly IRepository<ResumeTemplate, int> _templateRepository;

    public DeleteResumeTemplateHandler(IRepository<ResumeTemplate, int> templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<bool> Handle(DeleteResumeTemplateCommand command, CancellationToken cancellationToken)
    {
        await _templateRepository.SoftDeleteAsync(command.Id);
        return true;
    }
}