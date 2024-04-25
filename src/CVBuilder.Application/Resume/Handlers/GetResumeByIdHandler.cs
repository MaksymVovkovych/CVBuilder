using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses.Shared;
using CVBuilder.Models;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class GetResumeByIdHandler : IRequestHandler<GetResumeByIdQuery, ResumeResult>
{
    private readonly IRepository<Resume, int> _resumeRepository;
    private readonly IMapper _mapper;
    // private readonly IMemoryCache _cache;

    public GetResumeByIdHandler(IRepository<Resume, int> resumeRepository, IMapper mapper)
    {
        _resumeRepository = resumeRepository;
        _mapper = mapper;
        // _cache = cache;
    }

    public async Task<ResumeResult> Handle(GetResumeByIdQuery request, CancellationToken cancellationToken)
    {
        // var isResumeCached = _cache.TryGetValue($"resume-{request.Id}", out Resume resume);
        Resume resume = null;
        var resumeRequest = _resumeRepository.TableWithDeleted
            .Include(x => x.ResumeTemplate)
            .Include(x => x.Educations)
            .Include(x => x.Experiences)
            .ThenInclude(x => x.Skills)
            .ThenInclude(x => x.Skill)
            .Include(x => x.LevelSkills)
            .ThenInclude(l => l.Skill)
            .Include(x => x.Position)
            .Include(x => x.LevelLanguages)
            .ThenInclude(l => l.Language)
            .Include(x => x.ShortUrlIncognito)
            .Include(x => x.ShortUrlIncognitoWithoutLogo)
            .Include(x => x.ShortUrlFullResume)
            .Include(x => x.CreatedByUser)
            .Include(x => x.Owner)
            .AsSplitQuery()
            .AsNoTracking();

        resume ??= await resumeRequest.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken);


        if (resume == null) throw new NotFoundException("Resume not found");



        if (!request.IsByUrl)
        {
            if (request.UserRoles.Contains(RoleTypes.User.ToString()))
            {
                if (request.UserId != resume.CreatedByUserId)
                    throw new NotFoundException("Resume not found");
            }
        }


        // if (!isResumeCached)
            // _cache.Set($"resume-{resume.Id}", resume, TimeSpan.FromMinutes(5));

        var resumeResult = _mapper.Map<ResumeResult>(resume);

        return resumeResult;
    }
}