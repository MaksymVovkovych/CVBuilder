using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class ApproveProposalRequest
{
    public int ProposalId { get; set; }
    public List<ApproveProposalResumeRequest> Resumes { get; set; }
}