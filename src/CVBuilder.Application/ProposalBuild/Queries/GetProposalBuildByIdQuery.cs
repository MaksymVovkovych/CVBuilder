using CVBuilder.Application.ProposalBuild.Responses;
using MediatR;

namespace CVBuilder.Application.ProposalBuild.Queries;

public class GetProposalBuildByIdQuery : IRequest<ProposalBuildResult>
{
    public int ProposalBuildId { get; set; }
}