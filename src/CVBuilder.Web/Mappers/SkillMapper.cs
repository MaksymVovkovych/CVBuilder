using CVBuilder.Application.Resume.Responses.Shared;
using CVBuilder.Application.Skill.Commands;
using CVBuilder.Application.Skill.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Skill;
using CVBuilder.Web.Contracts.V1.Responses.Skill;

namespace CVBuilder.Web.Mappers;

public class SkillMapper : MapperBase
{
    public SkillMapper()
    {
        CreateMap<GetSkillByContainText, GetSkillByContainInTextQuery>();
        CreateMap<CreateSkillRequest, CreateSkillCommand>();
        CreateMap<UpdateSkillRequest, UpdateSkillCommand>();
        CreateMap<GetAllSkillRequest, GetAllSkillQuery>();
        CreateMap<LevelSkillResult, SkillResponse>()
            .ForMember(x => x.Name, y => y.MapFrom(z => z.SkillName));
    }
}