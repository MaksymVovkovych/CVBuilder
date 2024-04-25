using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.User.Queries;
using CVBuilder.Application.User.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Application.User.Handlers;

public class GetUsersByRoleHandler : IRequestHandler<GetUsersByRoleQuery, List<UserResult>>
{
    private readonly UserManager<Models.Entities.User> _manger;
    private readonly IMapper _mapper;

    public GetUsersByRoleHandler(UserManager<Models.Entities.User> manger, IMapper mapper)
    {
        _manger = manger;
        _mapper = mapper;
    }


    public async Task<List<UserResult>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
    {
        // var test = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == request.RoleName, cancellationToken: cancellationToken);
        var users = await _manger.GetUsersInRoleAsync(request.RoleName);
        var result = _mapper.Map<List<UserResult>>(users);
        return result;
    }
}