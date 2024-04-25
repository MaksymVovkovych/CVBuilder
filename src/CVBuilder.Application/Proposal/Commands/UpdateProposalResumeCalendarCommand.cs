using System;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Commands;

public class UpdateWorkDayCommand: IRequest<WorkDayResult>
{
    public int ProposalResumeId { get; set; }
    public UpdateWorkDay WorkDay { get; set; }
}

public class UpdateWorkDay
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int CountHours { get; set; }
}