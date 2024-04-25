using System;
using System.Collections.Generic;

namespace CVBuilder.Application.Resume.Responses.Shared;

public class ExperienceResult
{
    public int Id { get; set; }
    public string Company { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<SkillResult> Skills { get; set; }
}