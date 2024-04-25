using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Commands;

public class ApproveProposalCommand : IRequest<ProposalResult>
{
    public int ProposalId { get; set; }
    public List<ApproveProposalResumeCommand> Resumes { get; set; }
}