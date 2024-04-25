using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;

public class GetResumesByProposalBuildHandler : IRequestHandler<GetResumesByProposalBuildQuery, List<ResumeCardResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.ProposalBuild, int> _proposalBuildRepository;
    private readonly IRepository<Models.Entities.Resume, int> _resumeRepository;

    public GetResumesByProposalBuildHandler(IRepository<Models.Entities.Resume, int> resumeRepository,
        IMapper mapper, IRepository<Models.Entities.ProposalBuild, int> proposalBuildRepository)
    {
        _resumeRepository = resumeRepository;
        _mapper = mapper;
        _proposalBuildRepository = proposalBuildRepository;
    }

    public async Task<List<ResumeCardResult>> Handle(GetResumesByProposalBuildQuery request,
        CancellationToken cancellationToken)
    {
        var proposalBuild = await _proposalBuildRepository.Table
            .Include(x => x.Positions)
            .FirstOrDefaultAsync(x => x.Id == request.ProposalBuildId, cancellationToken);

        if (proposalBuild == null) throw new NotFoundException("Proposal Build not found");

        var positions = proposalBuild.Positions.Select(x => x.PositionId).ToList();

        var templates = await _resumeRepository.Table
            .Include(x => x.LevelSkills)
            .ThenInclude(x => x.Skill)
            .Include(x => x.Position)
            .Where(x => positions.Contains(x.PositionId.GetValueOrDefault()))
            .ToListAsync(cancellationToken);
        var result = _mapper.Map<List<ResumeCardResult>>(templates);
        return result;
    }
}