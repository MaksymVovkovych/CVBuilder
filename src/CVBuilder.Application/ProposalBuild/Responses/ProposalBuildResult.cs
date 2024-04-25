using System.Collections.Generic;

namespace CVBuilder.Application.ProposalBuild.Responses;

public class ProposalBuildResult
{
    public int Id { get; set; }
    public string ProjectTypeName { get; set; }
    public int Estimation { get; set; }
    public ComplexityResult Complexity { get; set; }
    public List<ProposalBuildPositionResult> Positions { get; set; }
}