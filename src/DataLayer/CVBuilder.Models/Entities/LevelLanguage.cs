using System;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class LevelLanguage : Entity<int>, IOrderlyEntity
{
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public LanguageLevel LanguageLevel { get; set; }
    public int Order { get; set; }
}