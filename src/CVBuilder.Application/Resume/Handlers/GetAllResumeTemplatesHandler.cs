using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;

public class GetAllResumeTemplatesHandler : IRequestHandler<GetAllResumeTemplatesQuery, List<ResumeTemplateCardResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ResumeTemplate, int> _repository;

    public GetAllResumeTemplatesHandler(IRepository<ResumeTemplate, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ResumeTemplateCardResult>> Handle(GetAllResumeTemplatesQuery request,
        CancellationToken cancellationToken)
    {
        var templates = await _repository.Table.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<ResumeTemplateCardResult>>(templates);
        return result;
    }
}