using CVBuilder.Application.Position.Responses;

namespace CVBuilder.Application.Position.Mapper;

public class CreatePositionMapper : AppMapperBase
{
    public CreatePositionMapper()
    {
        CreateMap<Models.Entities.Position, PositionResult>()
            .ForMember(x => x.PositionId, y => y.MapFrom(z => z.Id));
    }
}