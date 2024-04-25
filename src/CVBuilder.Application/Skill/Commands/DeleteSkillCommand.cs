using MediatR;

namespace CVBuilder.Application.Skill.Commands;

public class DeleteSkillCommand : IRequest<bool>
{
    public int SkillId { get; set; }
}