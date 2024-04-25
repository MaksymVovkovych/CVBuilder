using System.Linq;
using CVBuilder.Application.User.Responses;

namespace CVBuilder.Application.User.Mapper;

public class UserMapper : AppMapperBase
{
    public UserMapper()
    {
        CreateMap<Models.Entities.User, UserResult>()
            .ForMember(x => x.UserId, y => y.MapFrom(z => z.Id));

        CreateMap<Models.Entities.User, SmallUserResult>()
            .ForMember(x => x.Role, y => y.MapFrom(z => z.Roles.FirstOrDefault()))
            .ForMember(x => x.CreatedAt, y => y.MapFrom(z => z.CreatedAt.ToString("MM/dd/yyyy HH:mm:ss UTC")));

        CreateMap<Models.Entities.Role, UserRoleResult>();

        CreateMap<Models.Entities.User, UserResponse>();
    }
}