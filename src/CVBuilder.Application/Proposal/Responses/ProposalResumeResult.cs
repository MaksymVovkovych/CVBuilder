namespace CVBuilder.Application.Proposal.Responses;

public class ProposalResumeResult
{
    public bool ShowLogo { get; set; }
    public int ResumeTemplateId { get; set; }
    public Resume.Responses.Shared.ResumeResult Resume { get; set; }
}