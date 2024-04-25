using CVBuilder.Application.Complexity.Commands;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Complexity.Mappers;

public class CreateComplexityMapper : AppMapperBase
{
    public CreateComplexityMapper()
    {
        CreateMap<CreateComplexityCommand, ProposalBuildComplexity>();
    }
}