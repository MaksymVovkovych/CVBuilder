using System;

namespace CVBuilder.Application.Resume.Commands.SharedCommands;

public class CreateEducationCommand
{
    public string InstitutionName { get; set; }
    public string Specialization { get; set; }
    public string Degree { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}