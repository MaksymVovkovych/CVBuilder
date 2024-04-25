using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ProposalResume : Entity<int>
{
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public StatusProposalResume StatusResume { get; set; }
    public int? ShortUrlId { get; set; }
    public ShortUrl ShortUrl { get; set; }
    public int ProposalId { get; set; }
    public Proposal Proposal { get; set; }
    public List<WorkDay> WorkDays { get; set; }

}