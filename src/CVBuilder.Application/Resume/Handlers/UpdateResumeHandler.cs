using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses.Shared;
using CVBuilder.EFContext.Context;
using CVBuilder.Models;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

internal class UpdateResumeHandler : IRequestHandler<UpdateResumeCommand, ResumeResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Resume, int> _resumeRepository;
    private readonly IRepository<ResumeHistory, int> _resumeHistoryRepository;
    private readonly IMediator _mediator;
    // private readonly IMemoryCache _cache;
    private readonly IdentityEfDbContext _context;


    private readonly JsonSerializerOptions _options = new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public UpdateResumeHandler(IMapper mapper, IRepository<Resume, int> resumeRepository,
        IRepository<ResumeHistory, int> resumeHistoryRepository, IMediator mediator, IdentityEfDbContext context)
    {
        _resumeRepository = resumeRepository;
        _resumeHistoryRepository = resumeHistoryRepository;
        _mediator = mediator;
        // _cache = cache;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResumeResult> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
    {
        var resumeRequest = _mapper.Map<Resume>(request);
        var resumeDb = await _resumeRepository
            .Table
            .Include(x => x.Educations)
            .Include(x => x.Experiences)
            .ThenInclude(x => x.Skills)
            .Include(x => x.LevelLanguages)
            .Include(x => x.LevelSkills)
            .FirstOrDefaultAsync(x => x.Id == resumeRequest.Id, cancellationToken);

        if (resumeDb == null)
            throw new NotFoundException("Resume not found");

        var query = new GetResumeByIdQuery
        {
            Id = request.Id,
            UserId = request.UserId,
            UserRoles = request.UserRoles
        };

        var resumeResult = await _mediator.Send(query, cancellationToken);

        var resumeDbJson = JsonSerializer.Serialize(resumeResult, _options);
        MapFromRequest(resumeDb, resumeRequest);
        MapResumeExperience(resumeDb, resumeRequest);
        MapHiddenValues(resumeDb, resumeRequest);
        resumeDb.UpdatedByUserId = request.UserId;
        resumeDb = await _resumeRepository.UpdateAsync(resumeDb);
        // _cache.Remove($"resume-{resumeDb.Id}");

        // await UpdateResumeStatusForOther(resumeDb);

        var resumeResultNew = await _mediator.Send(query, cancellationToken);

        var resumeNewJson = JsonSerializer.Serialize(resumeResultNew, _options);

        await WriteHistory(resumeDbJson, resumeNewJson, request);

        return resumeResultNew;
    }

    
    private async Task WriteHistory(string resumeDbJson, string resumeNewJson, UpdateResumeCommand command)
    {
        var history = new ResumeHistory
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UpdateUserId = command.UserId,
            ResumeId = command.Id,
            OldResumeJson = resumeDbJson,
            NewResumeJson = resumeNewJson,
            Status = ResumeHistoryStatus.Updated
        };

        await _resumeHistoryRepository.CreateAsync(history);
    }

    private static void MapHiddenValues(Resume resumeDb, Resume resume)
    {
        resumeDb.AvailabilityStatus = resume.AvailabilityStatus;
        resumeDb.SalaryRate = resume.SalaryRate;
        resumeDb.CountDaysUnavailable = resume.CountDaysUnavailable;
    }

    private async Task UpdateResumeStatusForOther(Resume resumeDb)
    {
        if (resumeDb.OwnerId == null)
            return;

        var resumes = await _resumeRepository.Table
            .Where(x => x.OwnerId == resumeDb.OwnerId)
            .ToListAsync();

        foreach (var resume in resumes) resume.AvailabilityStatus = resumeDb.AvailabilityStatus;

        await _resumeRepository.UpdateRangeAsync(resumes);
    }

    private static void MapFromRequest(Resume resumeDb, Resume resume)
    {
        resumeDb.UpdatedAt = DateTime.UtcNow;
        resumeDb.AboutMe = resume.AboutMe;
        resumeDb.Hobbies = resume.Hobbies;
        resumeDb.Picture = resume.Picture;
        resumeDb.ResumeTemplateId = resume.ResumeTemplateId;
        resumeDb.ResumeName = resume.ResumeName;
        resumeDb.FirstName = resume.FirstName;
        resumeDb.LastName = resume.LastName;
        resumeDb.PositionId = resume.PositionId;
        resumeDb.PositionLevel = resume.PositionLevel;
        resumeDb.Email = resume.Email;
        resumeDb.Site = resume.Site;
        resumeDb.Phone = resume.Phone;
        resumeDb.Code = resume.Code;
        resumeDb.Country = resume.Country;
        resumeDb.City = resume.City;
        resumeDb.Street = resume.Street;
        resumeDb.RequiredPosition = resume.RequiredPosition;
        resumeDb.Birthdate = resume.Birthdate;
        resumeDb.Educations = resume.Educations;
        resumeDb.LevelLanguages = resume.LevelLanguages;
        resumeDb.LevelSkills = resume.LevelSkills;
        // resumeDb.Experiences = resume.Experiences;

      
        
    }

    private void MapResumeExperience(Resume resumeDb, Resume resume)
    {
        var experiencesDb = resumeDb.Experiences.Where(x => resume.Experiences.Any(y => y.Id == x.Id)).ToList();
        experiencesDb.AddRange(resume.Experiences.Where(x=>x.Id == 0));

        foreach (var experienceDb in experiencesDb)
        {
            if (experienceDb.Id == 0)
                continue;

            var experience = resume.Experiences.First(x => x.Id == experienceDb.Id);
            
            experienceDb.Company = experience.Company;
            experienceDb.Position = experience.Position;
            experienceDb.Description = experience.Description;
            experienceDb.StartDate = experience.StartDate;
            experienceDb.EndDate = experience.EndDate;
            experienceDb.UpdatedAt = DateTime.UtcNow;
            
            var skillsDb = experienceDb.Skills.Where(x => experience.Skills.Any(y => y.Id == x.Id)).ToList();
            skillsDb.AddRange(experience.Skills.Where(x=>x.Id ==0));

            foreach (var skillDb in skillsDb)
            {
                if(skillDb.Id == 0)
                    continue;

                var skill = experience.Skills.First(x => x.Id == skillDb.Id);

                skillDb.SkillId = skill.SkillId;
                skillDb.UpdatedAt = DateTime.UtcNow;
            }

            experienceDb.Skills = skillsDb;
        }

        resumeDb.Experiences = experiencesDb;
    }
    

}
