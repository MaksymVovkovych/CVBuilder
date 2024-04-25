using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.User.Queries;
using CVBuilder.Application.User.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.User.Handlers;

using Models.Entities;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, (int, List<SmallUserResult>)>
{
    private readonly IMapper _mapper;
    private readonly IRepository<User, int> _userRepository;

    public GetAllUsersHandler(IRepository<User, int> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<(int, List<SmallUserResult>)> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        IEnumerable<User> list =  await _userRepository.Table
            .Include(x => x.Roles)
            .ToListAsync(cancellationToken: cancellationToken);


        if (!string.IsNullOrWhiteSpace(request.Term))
        {
            var term = request.Term.ToLower();
            list = list.Where(p => (!string.IsNullOrEmpty(p.IdentityUser.FirstName)
                                    && p.IdentityUser.FirstName.ToLower().Contains(term))
                                   || (!string.IsNullOrEmpty(p.IdentityUser.LastName)
                                       && p.IdentityUser.LastName.ToLower().Contains(term))
                                   || (!string.IsNullOrEmpty(p.IdentityUser.Email) &&
                                       p.IdentityUser.Email.ToLower().Contains(term)));
                ;
        }

        var totalCount = list.Count();

        var page = request.Page;

        if (!string.IsNullOrWhiteSpace(request.Sort) && !string.IsNullOrWhiteSpace(request.Order))
            Sort(ref list, request);
        
        if (page.HasValue) page -= 1;

        list = list.Skip(page.GetValueOrDefault() * request.PageSize.GetValueOrDefault());

        if (request.PageSize != null)
            list = list.Take(request.PageSize.Value);
        

        var users = list.ToList();

        var usersResult = _mapper.Map<List<SmallUserResult>>(users);

        return (totalCount, usersResult);
    }

    private static void Sort(ref IEnumerable<User> query, GetAllUsersQuery request)
    {
        query = request.Sort switch
        {
            "fullName" => request.Order == "desc"
                ? query.OrderByDescending(p => p.IdentityUser.FirstName).ThenByDescending(p => p.IdentityUser.LastName)
                : query.OrderBy(p => p.IdentityUser.FirstName).ThenBy(p => p.IdentityUser.LastName),
            "email" => request.Order == "desc" ? query.OrderByDescending(p => p.IdentityUser.Email) : query.OrderBy(p => p.IdentityUser.Email),
            "role" => request.Order == "desc"
                ? query.OrderByDescending(p => p.Roles.FirstOrDefault().Name)
                : query.OrderBy(p => p.Roles.FirstOrDefault().Name),
            "createdAt" => request.Order == "desc"
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt),
            _ => query
        };
    }
}