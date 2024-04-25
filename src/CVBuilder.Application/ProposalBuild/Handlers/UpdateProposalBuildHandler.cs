using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.ProposalBuild.Commands;
using CVBuilder.Application.ProposalBuild.Queries;
using CVBuilder.Application.ProposalBuild.Responses;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.ProposalBuild.Handlers;

public class UpdateProposalBuildHandler : IRequestHandler<UpdateProposalBuildCommand, ProposalBuildResult>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<Models.Entities.ProposalBuild, int> _proposalBuildRepository;

    public UpdateProposalBuildHandler(IRepository<Models.Entities.ProposalBuild, int> proposalBuildRepository,
        IMapper mapper,
        IMediator mediator)
    {
        _proposalBuildRepository = proposalBuildRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ProposalBuildResult> Handle(UpdateProposalBuildCommand request,
        CancellationToken cancellationToken)
    {
        var proposalBuild = _mapper.Map<Models.Entities.ProposalBuild>(request);
        var proposalBuildDto = await _proposalBuildRepository.Table
            .Include(x => x.Complexity)
            .Include(x => x.Positions)
            .ThenInclude(x => x.Position)
            .FirstOrDefaultAsync(x => x.Id == proposalBuild.Id, cancellationToken);

        if (proposalBuildDto == null) throw new NotFoundException("Proposal Build not found");

        UpdateProposalBuild(proposalBuildDto, proposalBuild);
        proposalBuildDto = await _proposalBuildRepository.UpdateAsync(proposalBuildDto);
        var result = await _mediator
            .Send(new GetProposalBuildByIdQuery {ProposalBuildId = proposalBuildDto.Id}, cancellationToken);

        return result;
    }

    private void UpdateProposalBuild(Models.Entities.ProposalBuild proposalBuildDto,
        Models.Entities.ProposalBuild proposalBuild)
    {
        proposalBuildDto.UpdatedAt = DateTime.UtcNow;
        proposalBuildDto.Estimation = proposalBuild.Estimation;
        proposalBuildDto.ProjectTypeName = proposalBuild.ProjectTypeName;
        proposalBuildDto.ComplexityId = proposalBuild.ComplexityId;
        proposalBuildDto.Positions = proposalBuild.Positions;
    }
}