using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;

public class GetAllProposalsHandler : IRequestHandler<GetAllProposalsQuery, (int, List<SmallProposalResult>)>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Proposal, int> _proposalRepository;

    public GetAllProposalsHandler(IMapper mapper, IRepository<Proposal, int> proposalRepository)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
    }

    public async Task<(int, List<SmallProposalResult>)> Handle(GetAllProposalsQuery request,
        CancellationToken cancellationToken)
    {
        List<Proposal> proposals;
        var count = 0;


        IEnumerable<Proposal> list = await _proposalRepository.Table.Include(x => x.Resumes)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .ThenInclude(x => x.Position)
            .Include(x => x.CreatedUser)
            .Include(x => x.Client)
            .ToListAsync(cancellationToken: cancellationToken);

        var roles = new[]
        {
            RoleTypes.Admin.ToString(),
            RoleTypes.Hr.ToString(),
            RoleTypes.Sale.ToString(),
        };

        if (request.UserRoles.Intersect(roles).Any())
        {
            list = list.Where(x => x.StatusProposal != StatusProposal.Done);
        }
        else if (request.UserRoles.Contains(RoleTypes.Client.ToString()))
        {
            list = list.Where(x => x.ClientId == request.UserId);
        }
        else
        {
            proposals = new List<Proposal>();
            var emptyProposals = _mapper.Map<List<SmallProposalResult>>(proposals);

            return (count, emptyProposals);
        }

        if (!string.IsNullOrWhiteSpace(request.Term))
        {
            var term = request.Term.ToLower().Trim();
            list = list.Where(p => p.ProposalName.ToLower().Contains(term)
                                   || (p.Client.IdentityUser.FirstName != null
                                       && p.Client.IdentityUser.FirstName.ToLower().Contains(term))
                                   || (p.Client.IdentityUser.LastName != null
                                       && p.Client.IdentityUser.LastName.ToLower().Contains(term))
                                   || (p.CreatedUser.IdentityUser.FirstName != null
                                       && p.CreatedUser.IdentityUser.FirstName.ToLower().Contains(term))
                                   || (p.CreatedUser.IdentityUser.LastName != null
                                       && p.CreatedUser.IdentityUser.LastName.ToLower().Contains(term)));
        }

        if (!request.Clients.IsNullOrEmpty())
            list = list.Where(p => p.ClientId.HasValue && request.Clients.Contains(p.ClientId.Value));

        if (!request.Statuses.IsNullOrEmpty())
            list = list.Where(p => request.Statuses.Contains((int)p.StatusProposal));

        count = list.Count();

        var page = request.Page;
        if (page.HasValue) page -= 1;


        if (!string.IsNullOrWhiteSpace(request.Sort) && !string.IsNullOrWhiteSpace(request.Order))
            switch (request.Sort)
            {
                case "proposalName":
                {
                    list = request.Order == "desc"
                        ? list.OrderByDescending(p => p.ProposalName)
                        : list.OrderBy(p => p.ProposalName);
                }
                    break;
                case "clientUserName":
                {
                    list = request.Order == "desc"
                        ? list.OrderByDescending(p => p.Client.IdentityUser.FirstName).ThenByDescending(p => p.Client.IdentityUser.LastName)
                        : list.OrderBy(p => p.Client.IdentityUser.FirstName).ThenBy(p => p.Client.IdentityUser.LastName);
                }
                    break;
                case "proposalSize":
                {
                    list = request.Order == "desc"
                        ? list.OrderByDescending(p => p.Resumes.Count)
                        : list.OrderBy(p => p.Resumes.Count);
                }
                    break;
                case "showLogo":
                {
                    list = request.Order == "desc"
                        ? list.OrderByDescending(p => p.ShowLogo)
                        : list.OrderBy(p => p.ShowLogo);
                }
                    break;
                case "lastUpdated":
                {
                    list = request.Order == "desc"
                        ? list.OrderByDescending(p => p.UpdatedAt)
                        : list.OrderBy(p => p.UpdatedAt);
                }
                    break;
                case "createdUserName":
                {
                    list = request.Order == "desc"
                        ? list.OrderByDescending(p => p.CreatedUser.IdentityUser.FirstName)
                            .ThenByDescending(p => p.CreatedUser.IdentityUser.LastName)
                        : list.OrderBy(p => p.CreatedUser.IdentityUser.FirstName).ThenBy(p => p.CreatedUser.IdentityUser.LastName);
                }
                    break;
                case "statusProposal":
                {
                    list = request.Order == "desc"
                        ? list.OrderByDescending(p => p.StatusProposal)
                        : list.OrderBy(p => p.StatusProposal);
                }
                    break;
            }

        list = list.Skip(page.GetValueOrDefault() * request.PageSize.GetValueOrDefault());

        if (request.PageSize != null)
            list = list.Take(request.PageSize.Value);

        proposals = list.ToList();
        var smallProposals = _mapper.Map<List<SmallProposalResult>>(proposals);
        return (count, smallProposals);
    }
}