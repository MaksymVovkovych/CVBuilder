using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using EFCoreSecondLevelCacheInterceptor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class GetAllResumeCardHandler : IRequestHandler<GetAllResumeCardQuery, (int, List<ResumeCardResult>)>
{
    private readonly IRepository<Resume, int> _resumeRepository;
    private readonly IMapper _mapper;

    public GetAllResumeCardHandler(IRepository<Resume, int> resumeRepository, IMapper mapper)
    {
        _resumeRepository = resumeRepository;
        _mapper = mapper;
    }

    public async Task<(int, List<ResumeCardResult>)> Handle(GetAllResumeCardQuery request,
        CancellationToken cancellationToken)
    {
        var query = _resumeRepository.TableWithDeleted;

        query = request.IsArchive
            ? query.Where(x => x.DeletedAt.HasValue).OrderByDescending(x => x.DeletedAt)
            : query.Where(x => !x.DeletedAt.HasValue).OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

        IEnumerable<Resume> list = (await query.Include(x => x.Position)
                    .Include(x => x.ShortUrlFullResume)
                    .Include(x => x.ShortUrlIncognito)
                    .Include(x => x.ShortUrlIncognitoWithoutLogo)
                    .Include(x => x.ProposalResumes)
                    .ThenInclude(x => x.Proposal)
                    .ThenInclude(x => x.Client)
                    .Include(x => x.LevelSkills)
                    .ThenInclude(x => x.Skill)
                    .Include(x => x.CreatedByUser)
                    .Include(x => x.Owner)
                    .Include(x => x.Experiences)
                    .ThenInclude(x => x.Skills)
                .ToListAsync(cancellationToken: cancellationToken)
            );
        if (request.UserRoles.Contains(RoleTypes.Hr.ToString()))
        {
            // query = query.Where(x => x.IsDraft == false);
        }
        else if (request.UserRoles.Contains(RoleTypes.Admin.ToString()))
        {
        }
        else if (request.UserRoles.Contains(RoleTypes.User.ToString()))
        {
            list = list.Where(x => x.CreatedByUserId == request.UserId);
        }


        list = SearchByTerm(list, request.Term);

        list = FilterQuery(list, request.Positions, request.Skills, request.Clients, request.Statuses,
            request.Users);

        var totalCount = list.Count();

        list = SortQuery(list, request.Order, request.Sort);

        var page = request.Page;
        if (page.HasValue) page -= 1;

        list = list.Skip(page.GetValueOrDefault() * request.PageSize.GetValueOrDefault());
        
        
        if (request.PageSize != null)
            list = list.Take(request.PageSize.Value);

        var resumes = list.ToList();
        
        var results = _mapper.Map<List<ResumeCardResult>>(resumes);
        SortResumeClients(results);
        return (totalCount, results);
    }

    private static void SortResumeClients(List<ResumeCardResult> resumes)
    {
        foreach (var resume in resumes)
            resume.Clients = resume.Clients
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();
    }

    private static IEnumerable<Resume> SearchByTerm(IEnumerable<Resume> query,
        string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) 
            return query;
        
        var term = searchTerm.ToLower();
        
        return query.Where(r => r.FirstName.ToLower().Contains(term)
                                || r.LastName.ToLower().Contains(term)
                                || (r.Position != null && r.Position.PositionName
                                    .ToLower().Contains(term)));
    }


    private static IEnumerable<Resume> SortQuery(IEnumerable<Resume> query, string order,
        string sort)
    {
        if (string.IsNullOrWhiteSpace(sort) || string.IsNullOrWhiteSpace(order)) return query;

        switch (sort)
        {
            case "name":
            {
                query = order == "desc"
                    ? query.OrderByDescending(r => r.FirstName).ThenByDescending(r => r.LastName)
                    : query.OrderBy(r => r.FirstName).ThenBy(r => r.LastName);
            }
                break;
            case "position":
            {
                query = order == "desc"
                    ? query.OrderByDescending(r => r.Position.PositionName)
                    : query.OrderBy(r => r.Position.PositionName);
            }
                break;
            case "salaryRate":
            {
                query = order == "desc"
                    ? query.OrderByDescending(r => r.SalaryRateDecimal.GetValueOrDefault())
                    : query.OrderBy(r => r.SalaryRateDecimal.GetValueOrDefault());
            }
                break;
            default:
                return query;
        }

        return query;
    }

    private static IEnumerable<Resume> FilterQuery(
        IEnumerable<Resume> query,
        ICollection<int> positions,
        ICollection<int> skills,
        ICollection<int> clients,
        ICollection<int> statuses,
        ICollection<int> users)
    {
        if (!positions.IsNullOrEmpty())
            query = query.Where(r => r.PositionId.HasValue && positions.Contains(r.PositionId.Value));

        if (!skills.IsNullOrEmpty())
            query = query.Where(x =>
                x.LevelSkills.Any(y => skills.Contains(y.SkillId)) ||
                x.Experiences.Any(y => y.Skills.Any(z => skills.Contains(z.SkillId))));

        if (!clients.IsNullOrEmpty())
            query = query.Where(x => x.ProposalResumes.Any(y => clients.Contains(y.Proposal.ClientId.GetValueOrDefault())));

        if (!statuses.IsNullOrEmpty())
            query = query.Where(x => statuses.Contains((int)x.Owner.AvailabilityStatus.GetValueOrDefault()));

        if (!users.IsNullOrEmpty())
            query = query.Where(x => x.CreatedByUserId != null && users.Contains(x.CreatedByUserId.Value));
        return query;
    }
}