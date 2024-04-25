using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Position : Entity<int>
{
    public string PositionName { get; set; }
    public List<Resume> Resumes { get; set; }
    public List<ProposalBuildPosition> ProposalBuildPositions { get; set; }
}