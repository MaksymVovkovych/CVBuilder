namespace CVBuilder.Web.Contracts.V1.Requests.ProposalBuild;

public class UpdateProposalBuildPositionRequest
{
    public int Id { get; set; }
    public int PositionId { get; set; }
    public int CountMembers { get; set; }
}