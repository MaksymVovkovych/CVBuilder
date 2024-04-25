using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;
public class RecoverResumeHandler : IRequestHandler<RecoverResumeCommand, ResumeCardResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Resume, int> _resumeRepository;
    private readonly IRepository<ResumeHistory, int> _resumeHistoryRepository;
    // private readonly IMemoryCache _cache;
    
    public RecoverResumeHandler(
        IMapper mapper,
        IRepository<Resume, int> resumeRepository, IRepository<ResumeHistory, int> resumeHistoryRepository)
    {
        _resumeRepository = resumeRepository;
        _resumeHistoryRepository = resumeHistoryRepository;
        // _cache = cache;
        _mapper = mapper;
    }
    

    public async Task<ResumeCardResult> Handle(RecoverResumeCommand command, CancellationToken cancellationToken)
    {
        var resume = await _resumeRepository.RecoverAsync(command.Id);

        await WriteHistory(resume, command);
        // _cache.Remove($"resume-{resume.Id}");
        return _mapper.Map<ResumeCardResult>(resume);
    }
    
    private async Task WriteHistory(Resume resume,RecoverResumeCommand command)
    {
        var history = new ResumeHistory
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UpdateUserId = command.UserId,
            ResumeId = resume.Id,
            OldResumeJson = null,
            NewResumeJson = null,
            Status = ResumeHistoryStatus.Recovered
        };

        await _resumeHistoryRepository.CreateAsync(history);
    }
}