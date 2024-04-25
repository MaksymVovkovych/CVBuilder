using System.Collections.Generic;
using CVBuilder.Application.User.Responses;
using MediatR;

namespace CVBuilder.Application.User.Queries;

public class GetUsersByRoleQuery : IRequest<List<UserResult>>
{
    public string RoleName { get; set; }
}