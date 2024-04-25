using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Role.Queries;
using CVBuilder.Application.Role.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Role;

public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<RoleResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Role, int> _roleRepository;

    public GetAllRolesHandler(IRepository<Models.Entities.Role, int> roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<List<RoleResult>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.Table.ToListAsync(cancellationToken);

        var roleResult = _mapper.Map<List<RoleResult>>(roles);
        return roleResult;
    }
}