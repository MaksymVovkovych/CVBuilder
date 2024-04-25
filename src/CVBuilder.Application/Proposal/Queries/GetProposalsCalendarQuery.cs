using System;
using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class CalendarResult
{
    public List<DateTime> Days { get; set; }
    public List<ProposalCalendarResult> Proposals { get; set; }
}
public class GetProposalsCalendarQuery : IRequest<CalendarResult>
{
    public DateTime? Date { get; set; }
    public bool? IsWeekMode { get; set; }
    public int? Count { get; set; }
    public int? Skip { get; set; }
}