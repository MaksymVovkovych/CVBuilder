using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses.Shared;
using CVBuilder.Models;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class DuplicateResumeHandler : IRequestHandler<DuplicateResumeCommand, ResumeResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Resume, int> _resumeRepository;
    private readonly IRepository<ResumeHistory,int> _resumeHistoryRepository;
    private readonly JsonSerializerOptions _options = new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        WriteIndented = true
    };
    public DuplicateResumeHandler(IRepository<Resume, int> resumeRepository,
         IMapper mapper, IRepository<ResumeHistory, int> resumeHistoryRepository)
    {
        _resumeRepository = resumeRepository;
        _mapper = mapper;
        _resumeHistoryRepository = resumeHistoryRepository;
    }

    public async Task<ResumeResult> Handle(DuplicateResumeCommand request, CancellationToken cancellationToken)
    {
        var resumeRequest = _resumeRepository.Table
            .Include(x => x.Educations)
            .Include(x => x.Experiences)
            .Include(x => x.LevelSkills)
            .Include(x => x.LevelLanguages)
            .AsNoTracking();

        var resume = await resumeRequest.FirstOrDefaultAsync(x => x.Id == request.ResumeId,
            cancellationToken);

        if (resume == null) throw new NotFoundException("Resume not found");

        AssignNewResumeValues(resume);
        DuplicateArrays(resume);
        resume.CreatedByUserId = request.UserId;
        resume = await _resumeRepository.CreateAsync(resume);
        await WriteHistory(resume, request);

        var result = _mapper.Map<ResumeResult>(resume);
        return result;
    }

    private void AssignNewResumeValues(Resume resume)
    {
        resume.Id = 0;
        resume.CreatedAt = DateTime.UtcNow;
        resume.UpdatedAt = DateTime.UtcNow;
        resume.ShortUrlFullResumeId = 0;

        resume.ShortUrlFullResume = new ShortUrl
        {
        };

        resume.ShortUrlIncognitoId = 0;
        resume.ShortUrlIncognito = new ShortUrl
        {
        };

        resume.ShortUrlIncognitoWithoutLogoId = 0;
        resume.ShortUrlIncognitoWithoutLogo = new ShortUrl
        {
        };
    }

    private void DuplicateArrays(Resume resume)
    {
        resume.LevelSkills = resume.LevelSkills.Select(x =>
        {
            x.Id = 0;
            x.ResumeId = resume.Id;
            return x;
        }).ToList();
        resume.LevelLanguages = resume.LevelLanguages.Select(x =>
        {
            x.Id = 0;
            x.ResumeId = 0;
            return x;
        }).ToList();

        resume.Educations = resume.Educations.Select(x =>
        {
            x.Id = resume.Id;
            x.ResumeId = resume.Id;
            return x;
        }).ToList();
        ;
        resume.Experiences = resume.Experiences.Select(x =>
        {
            x.Id = resume.Id;
            x.ResumeId = resume.Id;
            return x;
        }).ToList();
    }
    
    private async Task WriteHistory(Resume resume,DuplicateResumeCommand command)
    {

        var history = new ResumeHistory
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UpdateUserId = command.UserId.GetValueOrDefault(),
            ResumeId = command.ResumeId,
            OldResumeJson = null,
            NewResumeJson = null,
            Status = ResumeHistoryStatus.Duplicated,
            DuplicatedResumeId = resume.Id
        };

        await _resumeHistoryRepository.CreateAsync(history);
    }
}
