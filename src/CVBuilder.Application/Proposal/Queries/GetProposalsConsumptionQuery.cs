using System;
using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalsConsumptionQuery: IRequest<ConsumptionResult>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int? ProposalId { get; set; }
}