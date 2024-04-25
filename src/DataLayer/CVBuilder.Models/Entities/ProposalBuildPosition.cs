using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ProposalBuildPosition : Entity<int>
{
    public int PositionId { get; set; }
    public int ProposalBuildId { get; set; }
    public ProposalBuild ProposalBuild { get; set; }
    public Position Position { get; set; }
    public int CountMembers { get; set; }
}