using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace CVBuilder.Application.Resume.Handlers;

public class GetResumeTemplateByIdHandler : IRequestHandler<GetTemplateByIdQuery, TemplateResult>
{
    private readonly IMapper _mapper;

    private readonly IRepository<ResumeTemplate, int> _templateRepository;

    // private readonly IMemoryCache _cache;
    public GetResumeTemplateByIdHandler(IRepository<ResumeTemplate, int> templateRepository, IMapper mapper)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
        // _cache = cache;
    }

    public async Task<TemplateResult> Handle(GetTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        // if(!_cache.TryGetValue($"resumeTemplate-{request.Id}", out TemplateResult templateResult))
        var template = await _templateRepository.GetByIdAsync(request.Id);

        if (template == null) throw new NotFoundException("Template not found");

        var templateResult = _mapper.Map<TemplateResult>(template);

        // _cache.Set($"resumeTemplate-{request.Id}", templateResult, 
        //     new MemoryCacheEntryOptions()
        //         .SetSlidingExpiration(TimeSpan.FromMinutes(5)));

        return templateResult;
    }
}