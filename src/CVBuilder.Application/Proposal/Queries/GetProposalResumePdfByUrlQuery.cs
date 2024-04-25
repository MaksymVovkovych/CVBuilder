using System.IO;
using CVBuilder.Application.Core.Infrastructure;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumePdfByUrlQuery : AuthorizedRequest<Stream>
{
    public string ShortUrl { get; set; }
}