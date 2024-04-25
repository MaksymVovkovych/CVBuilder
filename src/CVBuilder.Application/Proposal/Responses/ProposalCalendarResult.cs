using System.Collections.Generic;

namespace CVBuilder.Application.Proposal.Responses;

public class ProposalCalendarResult
{
    public int ProposalId { get; set; }
    public string ProposalName { get; set; }
    public List<ProposalResumeCalendarResult> Resumes { get; set; }
}