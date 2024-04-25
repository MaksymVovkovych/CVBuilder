using System.Collections.Generic;
using CVBuilder.Application.Position.Responses;

namespace CVBuilder.Application.Proposal.Responses;

public class ProposalResumeCalendarResult
{
    public int ProposalResumeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int SummaryHours { get; set; }
    public PositionResult Position { get; set; }
    public List<WorkDayResult> WorkDays { get; set; }
}