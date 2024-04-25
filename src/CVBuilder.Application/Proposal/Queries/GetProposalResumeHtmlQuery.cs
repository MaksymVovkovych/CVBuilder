using System.Collections.Generic;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public enum PrintFooter
{
    Print,
    NotPrint
}

public class GetProposalResumeHtmlQuery : IRequest<string>
{
    public List<string> UserRoles { get; set; }
    public int? UserId { get; set; }
    public int ProposalId { get; set; }
    public int ProposalResumeId { get; set; }
    public PrintFooter PrintFooter { get; set; } = PrintFooter.NotPrint;
}