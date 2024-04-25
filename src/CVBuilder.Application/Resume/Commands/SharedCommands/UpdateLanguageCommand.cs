namespace CVBuilder.Application.Resume.Commands.SharedCommands;

public class UpdateLanguageCommand
{
    public int? Id { get; set; }
    public int LanguageId { get; set; }
    public string LanguageName { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
}