using CVBuilder.Application.Complexity.Result;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Complexity.Mappers;

public class GetComplexityMapper : AppMapperBase
{
    public GetComplexityMapper()
    {
        CreateMap<ProposalBuildComplexity, ComplexityResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id));
    }
}