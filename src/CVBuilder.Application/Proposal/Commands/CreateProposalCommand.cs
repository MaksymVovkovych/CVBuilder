using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Commands;

public class CreateProposalCommand : IRequest<ProposalResult>
{
    public int UserId { get; set; }
    public int? ProposalBuildId { get; set; }
    public int ClientId { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowContacts { get; set; }
    public bool ShowCompanyNames { get; set; }
    public bool IsIncognito { get; set; }
    public string ProposalName { get; set; }
    public int ResumeTemplateId { get; set; }
    public List<CreateResumeCommand> Resumes { get; set; }
}