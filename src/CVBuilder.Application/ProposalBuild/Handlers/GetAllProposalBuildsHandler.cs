using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.ProposalBuild.Queries;
using CVBuilder.Application.ProposalBuild.Responses;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.ProposalBuild.Handlers;

public class GetAllProposalBuildsHandler : IRequestHandler<GetAllProposalBuildsQuery, List<ProposalBuildResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.ProposalBuild, int> _proposalBuildRepository;

    public GetAllProposalBuildsHandler(IRepository<Models.Entities.ProposalBuild, int> proposalBuildRepository,
        IMapper mapper)
    {
        _proposalBuildRepository = proposalBuildRepository;
        _mapper = mapper;
    }

    public async Task<List<ProposalBuildResult>> Handle(GetAllProposalBuildsQuery request,
        CancellationToken cancellationToken)
    {
        var proposalBuilds = await _proposalBuildRepository.Table
            .Include(x => x.Complexity)
            .Include(x => x.Positions)
            .ThenInclude(x => x.Position)
            .ToListAsync(cancellationToken);

        if (proposalBuilds == null) throw new NotFoundException("Proposal build not found");

        var result = _mapper.Map<List<ProposalBuildResult>>(proposalBuilds);
        return result;
    }
}