using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class WorkDay : Entity<int>
{
    public int ProposalResumeId { get; set; }
    public ProposalResume ProposalResume { get; set; }
    public DateTime Date { get; set; }
    public int CountHours { get; set; }
}