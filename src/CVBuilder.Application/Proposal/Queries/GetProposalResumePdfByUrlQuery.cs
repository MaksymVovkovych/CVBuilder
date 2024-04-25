using System.Collections.Generic;
using System.IO;
using CVBuilder.Application.Core.Infrastructure;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumePdfByUrlQuery : AuthorizedRequest<Stream>
{
    public string ShortUrl { get; set; }
}