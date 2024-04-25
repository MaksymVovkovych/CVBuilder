using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Skill : Entity<int>
{
    public string Name { get; set; }
    public IEnumerable<ExperienceSkill> Experiences { get; set; }
}