using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetResumesByProposalBuildQuery : IRequest<List<ResumeCardResult>>
{
    public int ProposalBuildId { get; set; }
}