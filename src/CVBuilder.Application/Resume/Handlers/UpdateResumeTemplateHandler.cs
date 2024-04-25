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

public class UpdateResumeTemplateHandler : IRequestHandler<UpdateResumeTemplateCommand, TemplateResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ResumeTemplate, int> _templateRepository;
    // private readonly IMemoryCache _cache;

    public UpdateResumeTemplateHandler(IRepository<ResumeTemplate, int> templateRepository, IMapper mapper)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
        // _cache = cache;
    }

    public async Task<TemplateResult> Handle(UpdateResumeTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetByIdAsync(request.TemplateId);

        if (template == null)
            throw new NotFoundException("Template not found");
        
        template.TemplateName = request.TemplateName;
        template.Html = request.Html;
        template.EditHtml = request.EditHtml;

        template = await _templateRepository.UpdateAsync(template);
        var model = _mapper.Map<TemplateResult>(template);
        
        
        // _cache.Set($"resumeTemplate-{request.TemplateId}", model, 
        //     new MemoryCacheEntryOptions()
        //         .SetSlidingExpiration(TimeSpan.FromMinutes(5)));
        return model;
    }
}