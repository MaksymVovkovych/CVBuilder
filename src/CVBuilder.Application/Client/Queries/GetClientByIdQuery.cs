using CVBuilder.Application.Client.Responses;
using MediatR;

namespace CVBuilder.Application.Client.Queries;

public class GetClientByIdQuery : IRequest<ClientResponse>
{
    public GetClientByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}