using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using MediatR;

namespace CVBuilder.Application.Proposal.Commands;

public class UpdateProposalCommand : IRequest<ProposalResult>
{
    public int Id { get; set; }
    public int? ClientId { get; set; }
    public StatusProposal StatusProposal { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowCompanyNames { get; set; }
    public bool IsIncognito { get; set; }
    public int ResumeTemplateId { get; set; }
    public int UserId { get; set; }
    public string ProposalName { get; set; }
    public List<UpdateResumeCommand> Resumes { get; set; }
}