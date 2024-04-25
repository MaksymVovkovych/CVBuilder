using System;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class DeleteResumeHandler : IRequestHandler<DeleteResumeCommand, bool>
{
    private readonly IRepository<Resume, int> _resumeRepository;
    private readonly IRepository<ResumeHistory, int> _resumeHistoryRepository;
    // private readonly IMemoryCache _cache;

    public DeleteResumeHandler(
        IRepository<Resume, int> resumeRepository, 
        IRepository<ResumeHistory, int> resumeHistoryRepository
        )
    {
        _resumeRepository = resumeRepository;
        _resumeHistoryRepository = resumeHistoryRepository;
        // _cache = cache;
    }

    public async Task<bool> Handle(DeleteResumeCommand command, CancellationToken cancellationToken)
    {
        await _resumeRepository.SoftDeleteAsync(command.Id);
        await WriteHistory(command);
        // _cache.Remove($"resume-{command.Id}");
        return true;
    }

    private async Task WriteHistory(DeleteResumeCommand command)
    {
        var history = new ResumeHistory
        {
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UpdateUserId = command.UserId,
            ResumeId = command.Id,
            OldResumeJson = null,
            NewResumeJson = null,
            Status = ResumeHistoryStatus.Deleted
        };

        await _resumeHistoryRepository.CreateAsync(history);
    }
}