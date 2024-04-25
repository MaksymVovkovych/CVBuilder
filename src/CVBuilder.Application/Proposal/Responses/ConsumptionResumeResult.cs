using System.Collections.Generic;
using CVBuilder.Application.Position.Responses;

namespace CVBuilder.Application.Proposal.Responses;

public class ConsumptionResumeResult
{
    public int ProposalResumeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Summary { get; set; }
    public PositionResult Position { get; set; }
    public List<decimal> SummaryOfMonths { get; set; }
}