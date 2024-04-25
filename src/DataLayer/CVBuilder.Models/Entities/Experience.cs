using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Experience : Entity<int>
{
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }
    [Encrypted]
    public string Company { get; set; }
    [Encrypted]
    public string Position { get; set; }
    [Encrypted]
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<ExperienceSkill> Skills { get; set; }
}