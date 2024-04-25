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
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Application.Resume.Handlers;
using Models.Entities;
internal class CreateResumeHandler : IRequestHandler<CreateResumeCommand, ResumeResult>
{
    private readonly IRepository<Language, int> _languageRepository;
    private readonly IMapper _mapper;
    private readonly IRepository<Resume, int> _resumeRepository;
    private readonly IRepository<Skill, int> _skillRepository;
    private readonly IRepository<ResumeHistory,int> _resumeHistoryRepository;
    private readonly IMediator _mediator;
    private readonly JsonSerializerOptions _options = new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    
    public CreateResumeHandler(
        IMapper mapper,
        IRepository<Resume, int> resumeRepository,
        IRepository<Skill, int> skillRepository,
        IRepository<Language, int> languageRepository,
        IRepository<ResumeHistory, int> resumeHistoryRepository,
        IMediator mediator)
    {
        _resumeRepository = resumeRepository;
        _skillRepository = skillRepository;
        _languageRepository = languageRepository;
        _resumeHistoryRepository = resumeHistoryRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ResumeResult> Handle(CreateResumeCommand command, CancellationToken cancellationToken)
    {
        var resume = _mapper.Map<Resume>(command);
        await CheckSkillsDuplicate(resume);
        await CheckLanguageDuplicate(resume);

        if (command.UserRoles.Contains(RoleTypes.Admin.ToString()))
            await AssignResumeToUser(resume);

        CreateShortUrlForResume(resume);

        resume.CreatedByUserId = command.UserId;
        resume.UpdatedByUserId = command.UserId;

        if (command.UserRoles.Contains(RoleTypes.User.ToString()))
            resume.OwnerId = command.UserId;

        resume = await _resumeRepository.CreateAsync(resume);

        await WriteHistory(resume, command);

        
        return _mapper.Map<ResumeResult>(resume);
    }

    private async Task WriteHistory(Resume resume,CreateResumeCommand command)
    {

        var resumeResult = await _mediator.Send(new GetResumeByIdQuery
        {
            Id = resume.Id,
            UserId = command.UserId,
            UserRoles = command.UserRoles
        });
        
        var resumeNewJson = JsonSerializer.Serialize(resumeResult, _options);

        var history = new ResumeHistory
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UpdateUserId = command.UserId,
            ResumeId = resume.Id,
            OldResumeJson = null,
            NewResumeJson = resumeNewJson,
            Status = ResumeHistoryStatus.Created
        };

        await _resumeHistoryRepository.CreateAsync(history);
    }

    private void CreateShortUrlForResume(Resume resume)
    {
        resume.ShortUrlFullResume = new ShortUrl
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        resume.ShortUrlIncognito = new ShortUrl
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        resume.ShortUrlIncognitoWithoutLogo = new ShortUrl
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    private async Task CheckLanguageDuplicate(Resume resume)
    {
        var allLanguage = await _languageRepository.GetListAsync();
        foreach (var cvLanguage in resume.LevelLanguages)
        {
            var language = allLanguage
                .FirstOrDefault(x => x.Id == cvLanguage.LanguageId || x.Name == cvLanguage.Language?.Name);
            if (language == null)
                continue;
            cvLanguage.Language = language;
            cvLanguage.LanguageId = language.Id;
        }
    }

    private async Task CheckSkillsDuplicate(Resume resume)
    {
        var allSkills = await _skillRepository.GetListAsync();
        foreach (var cvSkill in resume.LevelSkills)
        {
            var skill = allSkills.FirstOrDefault(x => x.Id == cvSkill.SkillId || x.Name == cvSkill.Skill?.Name);
            if (skill == null)
                continue;
            cvSkill.SkillId = skill.Id;
            cvSkill.Skill = skill;
        }
    }

    private async Task AssignResumeToUser(Resume resume)
    {

    }
}
