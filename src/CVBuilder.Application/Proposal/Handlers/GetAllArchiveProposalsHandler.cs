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

namespace CVBuilder.Application.Proposal.Handlers;

public class
    GetAllArchiveProposalsHandler : IRequestHandler<GetAllArchiveProposalsQuery, (int, List<SmallProposalResult>)>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Proposal, int> _proposalRepository;

    public GetAllArchiveProposalsHandler(IMapper mapper, IRepository<Models.Entities.Proposal, int> proposalRepository)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
    }

    public async Task<(int, List<SmallProposalResult>)> Handle(GetAllArchiveProposalsQuery request,
        CancellationToken cancellationToken)
    {
        var proposals = new List<Models.Entities.Proposal>();
        var count = 0;

        var query = _proposalRepository.Table;

        query = query.Where(x => x.StatusProposal == StatusProposal.Done);

        query = query.Include(x => x.Resumes)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .ThenInclude(x => x.Position)
            .Include(x => x.CreatedUser)
            .Include(x => x.Client);

        if (!string.IsNullOrWhiteSpace(request.Term))
        {
            var term = request.Term.ToLower();
            query = query.Where(p => p.ProposalName.ToLower().Contains(term)
                                     || p.Client.IdentityUser.FirstName.ToLower().Contains(term)
                                     || p.Client.IdentityUser.LastName.ToLower().Contains(term)
                                     || p.CreatedUser.IdentityUser.FirstName.ToLower().Contains(term)
                                     || p.CreatedUser.IdentityUser.LastName.ToLower().Contains(term));
        }

        if (request.Clients != null && request.Clients.Count > 0)
            query = query.Where(p => p.ClientId.HasValue && request.Clients.Contains(p.ClientId.Value));

        count = await query.CountAsync(cancellationToken);

        query = query.Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);

        if (!string.IsNullOrWhiteSpace(request.Sort) && !string.IsNullOrWhiteSpace(request.Order))
            switch (request.Sort)
            {
                case "proposalName":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.ProposalName)
                        : query.OrderBy(p => p.ProposalName);
                }
                    break;
                case "clientUserName":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.Client.IdentityUser.FirstName).ThenByDescending(p => p.Client.IdentityUser.LastName)
                        : query.OrderBy(p => p.Client.IdentityUser.FirstName).ThenBy(p => p.Client.IdentityUser.LastName);
                }
                    break;
                case "proposalSize":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.Resumes.Count)
                        : query.OrderBy(p => p.Resumes.Count);
                }
                    break;
                case "showLogo":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.ShowLogo)
                        : query.OrderBy(p => p.ShowLogo);
                }
                    break;
                case "lastUpdated":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.UpdatedAt)
                        : query.OrderBy(p => p.UpdatedAt);
                }
                    break;
                case "createdUserName":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.CreatedUser.IdentityUser.FirstName)
                            .ThenByDescending(p => p.CreatedUser.IdentityUser.LastName)
                        : query.OrderBy(p => p.CreatedUser.IdentityUser.FirstName).ThenBy(p => p.CreatedUser.IdentityUser.LastName);
                }
                    break;
                case "statusProposal":
                {
                    query = request.Order == "desc"
                        ? query.OrderByDescending(p => p.StatusProposal)
                        : query.OrderBy(p => p.StatusProposal);
                }
                    break;
            }

        proposals = await query.ToListAsync(cancellationToken);

        var smallProposals = _mapper.Map<List<SmallProposalResult>>(proposals);
        return (count, smallProposals);
    }
}