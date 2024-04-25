using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetProposalByIdHandler : IRequestHandler<GetProposalByIdQuery, ProposalResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Proposal, int> _proposalRepository;

    public GetProposalByIdHandler(IMapper mapper, IRepository<Models.Entities.Proposal, int> proposalRepository)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
    }

    public async Task<ProposalResult> Handle(GetProposalByIdQuery request, CancellationToken cancellationToken)
    {
        var proposal = await _proposalRepository.Table
            .Include(x => x.Client)
            .ThenInclude(x => x.ShortUrl)
            .Include(x => x.ProposalBuild)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .ThenInclude(x => x.LevelSkills)
            .ThenInclude(x => x.Skill)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .ThenInclude(x => x.Position)
            .Include(x => x.Client)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.ShortUrl)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (proposal == null) throw new NotFoundException("Proposal not found");

        var result = _mapper.Map<ProposalResult>(proposal);
        return result;
    }
}