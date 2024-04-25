using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Requests.ProposalBuild;

public class CreateProposalBuildRequest
{
    public string ProjectTypeName { get; set; }
    public int Estimation { get; set; }
    public int ComplexityId { get; set; }
    public List<CreateProposalBuildPositionRequest> Positions { get; set; }
}