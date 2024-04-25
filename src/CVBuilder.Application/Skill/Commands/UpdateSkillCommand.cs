using CVBuilder.Application.Skill.DTOs;
using MediatR;

namespace CVBuilder.Application.Skill.Commands;

public class UpdateSkillCommand : IRequest<SkillResult>
{
    public int SkillId { get; set; }
    public string SkillName { get; set; }
}