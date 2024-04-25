using CVBuilder.Application.User.Commands;
using CVBuilder.Application.User.Queries;
using CVBuilder.Application.User.Responses;
using CVBuilder.Web.Contracts.V1.Requests.User;
using UserResponse = CVBuilder.Web.Contracts.V1.Responses.CV.UserResponse;

namespace CVBuilder.Web.Mappers;

public class UserMapper : MapperBase
{
    public UserMapper()
    {
        CreateMap<GetAllUsersRequest, GetAllUsersQuery>();
        CreateMap<UpdateUserRequest, UpdateUserCommand>();
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<UserResult, UserResponse>();
        CreateMap<UpdateUserStatusRequest, UpdateUserStatusCommand>();
    }
}