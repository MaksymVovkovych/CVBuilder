﻿using System.IO;
using CVBuilder.Application.Core.Infrastructure;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalResumePdfByIdQuery : AuthorizedRequest<Stream>
{
    public int ProposalId { get; set; }
    public int ProposalResumeId { get; set; }

}