using System;
using CVBuilder.Application.ProposalBuild.Commands;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.ProposalBuild.Mappers;

public class CreateProposalBuildMapper : AppMapperBase
{
    public CreateProposalBuildMapper()
    {
        CreateMap<CreateProposalBuildCommand, Models.Entities.ProposalBuild>()
            .ForMember(x => x.CreatedAt, y => y.MapFrom(z => DateTime.UtcNow))
            .ForMember(x => x.UpdatedAt, y => y.MapFrom(z => DateTime.UtcNow));
        CreateMap<CreateProposalBuildPositionCommand, ProposalBuildPosition>();
    }
}