using System.Collections.Generic;
using CVBuilder.Application.Skill.DTOs;
using MediatR;

namespace CVBuilder.Application.Skill.Queries;

public class GetAllSkillQuery : IRequest<IEnumerable<SkillResult>>
{
}