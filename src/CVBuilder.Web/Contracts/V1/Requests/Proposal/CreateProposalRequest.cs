using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class CreateProposalRequest
{
    public string ProposalName { get; set; }
    public int ClientId { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowCompanyNames { get; set; }
    public bool IsIncognito { get; set; }
    public int ResumeTemplateId { get; set; }
    public int? ProposalBuildId { get; set; }
    public List<CreateResumeRequest> Resumes { get; set; }
}