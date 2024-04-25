using CVBuilder.Application.Position.Commands;
using CVBuilder.Web.Contracts.V1.Requests.Position;

namespace CVBuilder.Web.Mappers;

public class PositionMapper : MapperBase
{
    public PositionMapper()
    {
        CreateMap<CreatePositionRequest, CreatePositionCommand>();
        CreateMap<UpdatePositionRequest, UpdatePositionCommand>();
    }
}