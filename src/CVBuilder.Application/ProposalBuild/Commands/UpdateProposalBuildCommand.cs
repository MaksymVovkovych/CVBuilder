using System.Collections.Generic;
using CVBuilder.Application.ProposalBuild.Responses;
using MediatR;

namespace CVBuilder.Application.ProposalBuild.Commands;

public class UpdateProposalBuildCommand : IRequest<ProposalBuildResult>
{
    public int Id { get; set; }
    public string ProjectTypeName { get; set; }
    public int Estimation { get; set; }
    public int ComplexityId { get; set; }
    public List<UpdateProposalBuildPositionCommand> Positions { get; set; }
}