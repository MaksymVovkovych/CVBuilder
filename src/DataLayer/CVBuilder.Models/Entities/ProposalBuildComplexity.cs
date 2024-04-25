using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ProposalBuildComplexity :Entity<int>
{
    public string ComplexityName { get; set; }
    public List<ProposalBuild> ProposalBuilds { get; set; }
}