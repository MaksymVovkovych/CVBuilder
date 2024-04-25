using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ProposalBuild : Entity<int>
{
    public string ProjectTypeName { get; set; }
    public int Estimation { get; set; }
    public int? ComplexityId { get; set; }
    public ProposalBuildComplexity Complexity { get; set; }
    public List<ProposalBuildPosition> Positions { get; set; }

    public List<Proposal> Proposals { get; set; }
}