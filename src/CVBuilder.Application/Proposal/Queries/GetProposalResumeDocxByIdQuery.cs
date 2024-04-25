using System.Collections.Generic;
using System.IO;
using CVBuilder.Application.Core.Infrastructure;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumeDocxByIdQuery : AuthorizedRequest<Stream>
{
    public int ProposalId { get; set; }
    public int ProposalResumeId { get; set; }
}