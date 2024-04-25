using System;

namespace CVBuilder.Application.Proposal.Responses;

public class WorkDayResult
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int CountHours { get; set; }
}