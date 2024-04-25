using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ExperienceSkill: Entity<int>
{
    public int ExperienceId { get; set; }
    public Experience Experience { get; set; }
    public int SkillId { get; set; }
    public Skill Skill { get; set; }
}