using System;
using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Commands;

public class CreateTimePlanningCommand: IRequest<List<WorkDayResult>>
{
    public int ProposalResumeId { get; set; }
    public DateTime Date { get; set; }
    public bool? IsWeekMode { get; set; }
    public int CountHours { get; set; }
    public bool ExceptWeekends { get; set; }
    public bool ExceptHolidays { get; set; }
}