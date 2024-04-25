using CVBuilder.Application.ProposalBuild.Responses;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.ProposalBuild.Mappers;

public class ProposalBuildResultMapper : AppMapperBase
{
    public ProposalBuildResultMapper()
    {
        CreateMap<Models.Entities.ProposalBuild, ProposalBuildResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));

        CreateMap<ProposalBuildComplexity, ComplexityResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));

        CreateMap<ProposalBuildPosition, ProposalBuildPositionResult>()
            .ForMember(x => x.PositionName, y => y.MapFrom(x => x.Position.PositionName));
    }
}