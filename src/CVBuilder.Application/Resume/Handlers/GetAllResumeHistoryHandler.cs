using System.Collections.Generic;
using System.Linq;
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

public class GetAllResumeHistoryHandler : IRequestHandler<GetAllResumeHistoryQuery, List<ResumeHistoryResult>>
{
    private readonly IRepository<ResumeHistory, int> _resumeHistoryRepository;
    private readonly IMapper _mapper;

    public GetAllResumeHistoryHandler(IRepository<ResumeHistory, int> resumeHistoryRepository, IMapper mapper)
    {
        _resumeHistoryRepository = resumeHistoryRepository;
        _mapper = mapper;
    }

    public async Task<List<ResumeHistoryResult>> Handle(GetAllResumeHistoryQuery request,
        CancellationToken cancellationToken)
    {
        
        var histories = await _resumeHistoryRepository.Table
            .Include(x=>x.UpdateUser)
            .Where(x => x.ResumeId == request.ResumeId)
            .OrderByDescending(x=>x.UpdatedAt)
            .ToListAsync(cancellationToken: cancellationToken);

        var result = _mapper.Map<List<ResumeHistoryResult>>(histories);

        return result;
    }
}