using CVBuilder.Application.Role.Responses;

namespace CVBuilder.Application.Role.Mapper;

public class GetRoleMapper : AppMapperBase
{
    public GetRoleMapper()
    {
        CreateMap<Models.Entities.Role, RoleResult>();
    }
}