using System.Collections.Generic;
using CVBuilder.Models;

namespace CVBuilder.Application.Proposal.Responses;

public class ProposalResult
{
    public int Id { get; set; }
    public ProposalClientResult Client { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowCompanyNames { get; set; }
    public bool IsIncognito { get; set; }
    public int ResumeTemplateId { get; set; }
    public StatusProposal StatusProposal { get; set; }
    public string ProposalName { get; set; }
    public List<ResumeResult> Resumes { get; set; }
}