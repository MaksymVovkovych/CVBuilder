﻿using CVBuilder.Application.Core.Infrastructure;
using CVBuilder.Application.Proposal.Responses;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumeByUrlQuery : AuthorizedRequest<ProposalResumeResult>
{
    public string ShortUrl { get; set; }
}