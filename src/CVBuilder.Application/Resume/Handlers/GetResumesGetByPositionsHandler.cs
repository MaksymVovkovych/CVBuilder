using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;

public class GetResumesGetByPositionsHandler : IRequestHandler<GetResumesByPositionQuery, List<ResumeCardResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Resume, int> _repository;

    public GetResumesGetByPositionsHandler(IRepository<Models.Entities.Resume, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ResumeCardResult>> Handle(GetResumesByPositionQuery request,
        CancellationToken cancellationToken)
    {
        var templates = await _repository.Table
            .Include(x => x.LevelSkills)
            .ThenInclude(x => x.Skill)
            .Include(x => x.Position)
            .Where(x => request.Positions.Contains(x.Position.PositionName.ToLower()))
            .ToListAsync(cancellationToken);
        var result = _mapper.Map<List<ResumeCardResult>>(templates);
        return result;
    }
}