using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Skill.Commands;
using CVBuilder.Application.Skill.DTOs;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Skill.Handlers;
using Models.Entities;
internal class CreateSkillHandler : IRequestHandler<CreateSkillCommand, SkillResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Skill, int> _skillRepository;

    public CreateSkillHandler(IRepository<Skill, int> cvRepository, IMapper mapper)
    {
        _skillRepository = cvRepository;
        _mapper = mapper;
    }

    public async Task<SkillResult> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.SkillName)) request.SkillName = "";
        
        var model = new Skill
        {
            Name = request.SkillName,
        };

        var skill = await _skillRepository.CreateAsync(model);

        return _mapper.Map<SkillResult>(skill);
    }
}