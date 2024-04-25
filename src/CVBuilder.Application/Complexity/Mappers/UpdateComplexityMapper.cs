using System;
using CVBuilder.Application.Complexity.Commands;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Complexity.Mappers;

public class UpdateComplexityMapper : AppMapperBase
{
    public UpdateComplexityMapper()
    {
        CreateMap<UpdateComplexityCommand, ProposalBuildComplexity>()
            .ForMember(x => x.UpdatedAt, y => y.MapFrom(z => DateTime.UtcNow));
    }
}