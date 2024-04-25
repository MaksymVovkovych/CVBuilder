using System.Collections.Generic;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Models;

namespace CVBuilder.Application.Proposal.Responses;

public class SmallProposalResult
{
    public int Id { get; set; }
    public string ProposalName { get; set; }
    public string ClientUserName { get; set; }
    public int ProposalSize { get; set; }
    public bool ShowLogo { get; set; }
    public bool ShowContacts { get; set; }
    public bool ShowCompanyNames { get; set; }
    public string LastUpdated { get; set; }
    public string CreatedUserName { get; set; }
    public List<PositionResult> Positions { get; set; }
    public StatusProposal StatusProposal { get; set; }
}