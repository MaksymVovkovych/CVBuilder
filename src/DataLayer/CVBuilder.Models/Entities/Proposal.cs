using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Proposal : Entity<int>
{
    public int? CreatedUserId { get; set; }
    public User CreatedUser { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowCompanyNames { get; set; }
    public bool IsIncognito { get; set; }
    public int? ClientId { get; set; }
    public User Client { get; set; } 
    [Encrypted]
    public string ProposalName { get; set; }
    public int ResumeTemplateId { get; set; }
    public ResumeTemplate ResumeTemplate { get; set; }

    public int? ProposalBuildId { get; set; }
    public ProposalBuild ProposalBuild { get; set; }
    public List<ProposalResume> Resumes { get; set; }
    public StatusProposal StatusProposal { get; set; }
}