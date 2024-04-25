using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Skill.Commands;
using CVBuilder.Application.Skill.DTOs;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Skill.Handlers;
using Models.Entities;
public class UpdateSkillHandler : IRequestHandler<UpdateSkillCommand, SkillResult>
{
    private readonly IRepository<Skill, int> _skillRepository;
    private readonly IMapper _mapper;

    public UpdateSkillHandler(IRepository<Skill, int> skillRepository, IMapper mapper)
    {
        _skillRepository = skillRepository;
        _mapper = mapper;
    }

    public async Task<SkillResult> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var model = new Skill
        {
            Id = request.SkillId,
            Name = request.SkillName,
            UpdatedAt = DateTime.Now
        };

        var skill = await _skillRepository.UpdateAsync(model);
        return _mapper.Map<SkillResult>(skill);
    }
}