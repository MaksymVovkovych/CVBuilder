using CVBuilder.Application.Skill.DTOs;

namespace CVBuilder.Application.Skill.Mapper;

using Models.Entities;

internal class SkillMapper : AppMapperBase
{
    public SkillMapper()
    {
        CreateMap<Skill, SkillResult>()
            .ForMember(x => x.SkillId, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.SkillName, y => y.MapFrom(z => z.Name));
    }
}