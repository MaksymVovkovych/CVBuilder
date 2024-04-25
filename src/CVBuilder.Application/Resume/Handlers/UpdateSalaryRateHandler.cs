using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Resume.Handlers;

public class UpdateSalaryRateHandler : IRequestHandler<UpdateSalaryRateResumeCommand, ResumeCardResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Models.Entities.Resume, int> _resumeRepository;

    public UpdateSalaryRateHandler(IMapper mapper, IRepository<Models.Entities.Resume, int> resumeRepository)
    {
        _resumeRepository = resumeRepository;
        _mapper = mapper;
    }

    public async Task<ResumeCardResult> Handle(UpdateSalaryRateResumeCommand request,
        CancellationToken cancellationToken)
    {
        var resumeDb = await _resumeRepository.Table
            .Include(x => x.ResumeTemplate)
            .Include(x => x.Educations)
            .Include(x => x.Experiences)
            .Include(x => x.LevelSkills)
            .ThenInclude(l => l.Skill)
            .Include(x => x.Position)
            .Include(x => x.LevelLanguages)
            .ThenInclude(l => l.Language)
            .FirstOrDefaultAsync(x => x.Id == request.ResumeId, cancellationToken);

        if (resumeDb == null)
            throw new NotFoundException("Resume not found");

        resumeDb.SalaryRateDecimal = request.SalaryRate;
        resumeDb.Id = 0;
        resumeDb = await _resumeRepository.CreateAsync(resumeDb);
        return _mapper.Map<ResumeCardResult>(resumeDb);
    }
}