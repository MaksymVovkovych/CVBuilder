﻿using System;
using System.Collections.Generic;

namespace CVBuilder.Application.Resume.Commands.SharedCommands;

public class CreateExperienceCommand
{
    public string Company { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<ExperienceSkillCommand> Skills { get; set; }
}