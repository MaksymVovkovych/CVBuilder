using CVBuilder.Application.Skill.DTOs;
using MediatR;

namespace CVBuilder.Application.Skill.Commands;

public class CreateSkillCommand : IRequest<SkillResult>
{
    public string SkillName { get; set; }
}