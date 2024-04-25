using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Models.Entities;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers;

public class GetDocxTemplateByIdHandler : IRequestHandler<GetDocxTemplateByIdQueries, byte[]>
{
    private readonly IRepository<ResumeTemplate, int> _templateRepository;

    public GetDocxTemplateByIdHandler(IRepository<ResumeTemplate, int> templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<byte[]> Handle(GetDocxTemplateByIdQueries request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetByIdAsync(request.TemplateId);

        if (template?.Docx == null)
            throw new NotFoundException("Template not found");

        return template.Docx;
    }
}