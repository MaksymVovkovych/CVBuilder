using System;
using System.Collections.Generic;

namespace CVBuilder.Application.Proposal.Responses;

public class ConsumptionResult
{
    public List<DateTime> Months { get; set; }
    public List<ProposalConsumptionResult> Proposals { get; set; }
}