using System;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class GetProposalsCalendarRequest
{
    public int? Count { get; set; }
    public int? Skip { get; set; }
    public DateTime? Date { get; set; }
    public bool? IsWeekMode { get; set; }
}