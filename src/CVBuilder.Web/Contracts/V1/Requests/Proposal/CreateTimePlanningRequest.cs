using System;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class CreateTimePlanningRequest
{
    public int ProposalResumeId { get; set; }
    public DateTime Date { get; set; }
    public bool? IsWeekMode { get; set; }
    public int CountHours { get; set; }
    public bool ExceptWeekends { get; set; }
    public bool ExceptHolidays { get; set; }
}