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

public class GetProposalBuildByIdHandler : IRequestHandler<GetProposalBuildByIdQuery, ProposalBuildResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.ProposalBuild, int> _proposalBuildRepository;

    public GetProposalBuildByIdHandler(IRepository<Models.Entities.ProposalBuild, int> proposalBuildRepository,
        IMapper mapper)
    {
        _proposalBuildRepository = proposalBuildRepository;
        _mapper = mapper;
    }

    public async Task<ProposalBuildResult> Handle(GetProposalBuildByIdQuery request,
        CancellationToken cancellationToken)
    {
        var proposalBuild = await _proposalBuildRepository.Table
            .Include(x => x.Complexity)
            .Include(x => x.Positions)
            .ThenInclude(x => x.Position)
            .FirstOrDefaultAsync(x => x.Id == request.ProposalBuildId, cancellationToken);

        if (proposalBuild == null) throw new NotFoundException("Proposal build not found");

        var result = _mapper.Map<ProposalBuildResult>(proposalBuild);
        return result;
    }
}