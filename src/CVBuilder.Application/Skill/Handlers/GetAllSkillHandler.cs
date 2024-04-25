using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Skill.DTOs;
using CVBuilder.Application.Skill.Queries;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Skill.Handlers;
using Models.Entities;
public class GetAllSkillHandle : IRequestHandler<GetAllSkillQuery, IEnumerable<SkillResult>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Skill, int> _skillRepository;

    public GetAllSkillHandle(IRepository<Skill, int> cvRepository, IMapper mapper)
    {
        _skillRepository = cvRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<SkillResult>> Handle(GetAllSkillQuery request,
        CancellationToken cancellationToken)
    {
        var skills = await _skillRepository
            .Table.OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<SkillResult>>(skills);

        return result;
    }
}