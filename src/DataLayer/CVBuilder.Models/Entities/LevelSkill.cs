using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class LevelSkill : Entity<int>, IOrderlyEntity
{
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public int SkillId { get; set; }
    public Skill Skill { get; set; }
    public SkillLevel SkillLevel { get; set; }
    public int Order { get; set; }
}