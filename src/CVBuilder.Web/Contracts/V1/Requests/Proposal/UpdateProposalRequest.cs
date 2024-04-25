using System.Collections.Generic;
using CVBuilder.Models;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class UpdateProposalRequest
{
    public int Id { get; set; }
    public int? ClientId { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowCompanyNames { get; set; }
    public bool IsIncognito { get; set; }
    public int ResumeTemplateId { get; set; }
    public StatusProposal StatusProposal { get; set; }
    public string ProposalName { get; set; }
    public List<UpdateResumeRequest> Resumes { get; set; }
}