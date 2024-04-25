using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers;

public class UpdateDocxInTemplateHandler : IRequestHandler<UpdateDocxInTemplateCommand, TemplateResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ResumeTemplate, int> _templateRepository;

    public UpdateDocxInTemplateHandler(IRepository<ResumeTemplate, int> templateRepository, IMapper mapper)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
    }

    public async Task<TemplateResult> Handle(UpdateDocxInTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetByIdAsync(request.TemplateId);

        if (template == null) throw new NotFoundException("Template not found");

        template.Docx = request.Data;

        template = await _templateRepository.UpdateAsync(template);

        var model = _mapper.Map<TemplateResult>(template);

        return model;
    }
}