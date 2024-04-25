using System.Collections.Generic;
using CVBuilder.Application.Skill.DTOs;
using MediatR;

namespace CVBuilder.Application.Skill.Queries;

public class GetSkillByContainInTextQuery : IRequest<IEnumerable<SkillResult>>
{
    public string Content { get; set; }
}