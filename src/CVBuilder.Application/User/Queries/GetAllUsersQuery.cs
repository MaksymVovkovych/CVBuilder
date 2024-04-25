using System.Collections.Generic;
using CVBuilder.Application.User.Responses;
using MediatR;

namespace CVBuilder.Application.User.Queries;

public class GetAllUsersQuery : IRequest<(int, List<SmallUserResult>)>
{
    public string Term { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string Sort { get; set; }
    public string Order { get; set; }
}