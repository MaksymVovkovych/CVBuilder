using System;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class GetProposalsConsumptionRequest
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int? ProposalId { get; set; }
}