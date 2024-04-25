using System.Collections.Generic;
using CVBuilder.Application.ProposalBuild.Responses;
using MediatR;

namespace CVBuilder.Application.ProposalBuild.Commands;

public class CreateProposalBuildCommand : IRequest<ProposalBuildResult>
{
    public string ProjectTypeName { get; set; }
    public int Estimation { get; set; }
    public int ComplexityId { get; set; }
    public List<CreateProposalBuildPositionCommand> Positions { get; set; }
}