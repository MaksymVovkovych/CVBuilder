using CVBuilder.Application.User.Responses;
using MediatR;

namespace CVBuilder.Application.User.Queries;

public class GetUserByIdQuery : IRequest<UserResponse>
{
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}