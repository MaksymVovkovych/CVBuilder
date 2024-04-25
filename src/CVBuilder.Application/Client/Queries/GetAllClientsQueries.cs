using System.Collections.Generic;
using CVBuilder.Application.Client.Responses;
using MediatR;

namespace CVBuilder.Application.Client.Queries;

public class GetAllClientsQueries : IRequest<(int, List<ClientListItemResponse>)>
{
    public string Term { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string Sort { get; set; }
    public string Order { get; set; }
}