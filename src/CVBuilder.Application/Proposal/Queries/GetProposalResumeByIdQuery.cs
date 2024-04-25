using CVBuilder.Application.Core.Infrastructure;
using CVBuilder.Application.Proposal.Responses;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumeByIdQuery : AuthorizedRequest<ProposalResumeResult>
{
    public int ProposalId { get; set; }
    public int ProposalResumeId { get; set; }
}