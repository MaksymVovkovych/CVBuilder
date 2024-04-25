using System.Collections.Generic;
using CVBuilder.Application.ProposalBuild.Responses;
using MediatR;

namespace CVBuilder.Application.ProposalBuild.Queries;

public class GetAllProposalBuildsQuery : IRequest<List<ProposalBuildResult>>
{
}