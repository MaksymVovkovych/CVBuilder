using MediatR;

namespace CVBuilder.Application.Proposal.Commands;

public class RecoverProposalCommand: IRequest<bool>
{
    public int ProposalId { get; set; }   
}