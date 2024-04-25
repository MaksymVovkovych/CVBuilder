using System.IO;
using CVBuilder.Application.Core.Infrastructure;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumeDocxByUrlQuery : AuthorizedRequest<Stream>
{
    public string ShortUrl { get; set; }
}