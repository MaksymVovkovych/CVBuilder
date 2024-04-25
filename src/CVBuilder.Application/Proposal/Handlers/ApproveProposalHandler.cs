using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

public class ApproveProposalHandler : IRequestHandler<ApproveProposalCommand, ProposalResult>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<Models.Entities.Proposal, int> _proposalRepository;

    public ApproveProposalHandler(IMapper mapper, IRepository<Models.Entities.Proposal, int> proposalRepository,
        IMediator mediator)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
        _mediator = mediator;
    }

    public async Task<ProposalResult> Handle(ApproveProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = await _proposalRepository.Table
            .Include(x => x.Resumes)
            .FirstOrDefaultAsync(x => x.Id == request.ProposalId, cancellationToken);

        if (proposal == null) throw new NotFoundException("Proposal not found");

        foreach (var resume in proposal.Resumes)
        {
            var resumeRequest = request.Resumes.FirstOrDefault(x => x.Id == resume.Id);
            if (resumeRequest == null)
                resume.StatusResume = StatusProposalResume.NotSelected;
            else
                resume.StatusResume = resumeRequest.IsSelected
                    ? StatusProposalResume.Selected
                    : StatusProposalResume.Denied;
        }

        proposal.StatusProposal = proposal.Resumes.Any(x => x.StatusResume == StatusProposalResume.Selected)
            ? StatusProposal.Approved
            : StatusProposal.Denied;

        proposal.UpdatedAt = DateTime.UtcNow;

        proposal = await _proposalRepository.UpdateAsync(proposal);
        return await _mediator.Send(new GetProposalByIdQuery {Id = proposal.Id}, cancellationToken);
    }
}