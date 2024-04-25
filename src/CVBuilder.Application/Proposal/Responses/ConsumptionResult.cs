using System;
using System.Collections.Generic;
using CVBuilder.Application.Position.Responses;

namespace CVBuilder.Application.Proposal.Responses;

public class ConsumptionResult
{
    public List<DateTime> Months { get; set; }
    public List<ProposalConsumptionResult> Proposals { get; set; }
}