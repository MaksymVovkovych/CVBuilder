using System.Collections.Generic;

namespace CVBuilder.Application.Proposal.Responses;

public class ProposalConsumptionResult
{
    public int ProposalId { get; set; }
    public string ProposalName { get; set; }
    public decimal Summary { get; set; }
    public List<decimal> SummaryByMonths { get; set; }
    public List<ConsumptionResumeResult> Resumes { get; set; }
}