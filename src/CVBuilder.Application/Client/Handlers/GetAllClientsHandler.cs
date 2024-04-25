using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Client.Queries;
using CVBuilder.Application.Client.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Client.Handlers;
using Models.Entities;
public class GetAllClientsHandler : IRequestHandler<GetAllClientsQueries, (int, List<ClientListItemResponse>)>
{
    private readonly IMapper _mapper;
    private readonly IRepository<User, int> _userRepository;

    public GetAllClientsHandler(IMapper mapper, IRepository<User, int> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }


    public async Task<(int, List<ClientListItemResponse>)> Handle(GetAllClientsQueries request,
        CancellationToken cancellationToken)
    {
        var query = _userRepository.Table;

        query = query.Where(u =>
            u.Roles.Any(r => r.NormalizedName.Contains(RoleTypes.Client.ToString().ToUpper())));
        query = query.Include(u => u.ClientProposals)
            ;

        IEnumerable<User> list = await query.ToListAsync(cancellationToken: cancellationToken);

        SearchByTerm(ref list, request.Term);

        var totalCount = list.Count();

        SortClients(ref list, request.Order, request.Sort);

        var page = request.Page;
        if (page.HasValue) page -= 1;

        list = list.Skip(page.GetValueOrDefault() * request.PageSize.GetValueOrDefault());

        if (request.PageSize != null)
            list = list.Take(request.PageSize.Value);

        list = list.OrderBy(x => x.IdentityUser.FirstName).ThenBy(x => x.IdentityUser.LastName);

        var resultList = list.ToList();

        var result = _mapper.Map<List<ClientListItemResponse>>(resultList);
        return (totalCount, result);
    }

    private static void SearchByTerm(ref IEnumerable<User> query, string term)
    {
        if (!string.IsNullOrWhiteSpace(term))
        {
            var lowerTerm = term.ToLower();
            query = query.Where(u => (u.IdentityUser.FirstName != null && u.IdentityUser.FirstName.ToLower().Contains(lowerTerm))
                                     || (u.IdentityUser.LastName != null && u.IdentityUser.LastName.ToLower().Contains(lowerTerm))
                                     || (u.IdentityUser.Email != null && u.IdentityUser.Email.ToLower().Contains(lowerTerm))
                                     || (u.IdentityUser.PhoneNumber != null && u.IdentityUser.PhoneNumber.ToLower().Contains(lowerTerm))
            );
        }
    }

    private static void SortClients(ref IEnumerable<User> query, string sortDirections, string columnName)
    {
        if (!string.IsNullOrWhiteSpace(sortDirections) && !string.IsNullOrWhiteSpace(columnName))
            switch (columnName)
            {
                case "fullName":
                {
                    query = sortDirections == "desc"
                        ? query.OrderByDescending(r => r.IdentityUser.FirstName).ThenByDescending(r => r.IdentityUser.LastName)
                        : query.OrderBy(r => r.IdentityUser.FirstName).ThenBy(r => r.IdentityUser.LastName);
                }
                    break;
                case "email":
                {
                    query = sortDirections == "desc"
                        ? query.OrderBy(u => u.IdentityUser.Email)
                        : query.OrderByDescending(u => u.IdentityUser.Email);
                }
                    break;
                case "phoneNumber":
                {
                    query = sortDirections == "desc"
                        ? query.OrderBy(u => u.IdentityUser.PhoneNumber)
                        : query.OrderByDescending(u => u.IdentityUser.PhoneNumber);
                }
                    break;
                case "site":
                {
                    query = sortDirections == "desc"
                        ? query.OrderBy(u => u.Site)
                        : query.OrderByDescending(u => u.Site);
                }
                    break;
                case "companyName":
                {
                    query = sortDirections == "desc"
                        ? query.OrderBy(u => u.CompanyName)
                        : query.OrderByDescending(u => u.CompanyName);
                }
                    break;
            }
    }
}