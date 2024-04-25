namespace CVBuilder.Application.Resume.Commands.SharedCommands;

public class UpdateSkillCommand
{
    public int? Id { get; set; }
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
}