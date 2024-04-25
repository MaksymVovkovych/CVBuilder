using System.Collections.Generic;
using CVBuilder.Application.Role.Responses;
using MediatR;

namespace CVBuilder.Application.Role.Queries;

public class GetAllRolesQuery : IRequest<List<RoleResult>>
{
}