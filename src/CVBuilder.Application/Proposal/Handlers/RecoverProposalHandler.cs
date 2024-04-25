using System;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Models;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;
using Models.Entities;

public class RecoverProposalHandler: IRequestHandler<RecoverProposalCommand,bool>
{
    private readonly IRepository<Proposal, int> _proposalRepository;

    public RecoverProposalHandler(IRepository<Proposal, int> proposalRepository)
    {
        _proposalRepository = proposalRepository;
    }

    public async Task<bool> Handle(RecoverProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = await _proposalRepository.Table
            .FirstOrDefaultAsync(x => x.Id == request.ProposalId, cancellationToken: cancellationToken);

        if (proposal == null)
            throw new NotFoundException("Proposal not found");

        proposal.StatusProposal = StatusProposal.InReview;
        proposal.UpdatedAt = DateTime.UtcNow;

        await _proposalRepository.UpdateAsync(proposal);

        return true;
    }
}