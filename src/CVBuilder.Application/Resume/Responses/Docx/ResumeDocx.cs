using System.Collections.Generic;

namespace CVBuilder.Application.Resume.Responses.Docx;

public class ResumeDocx
{
    public int Id { get; set; }
    public string ResumeName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PositionName { get; set; }
    public string PositionLevel { get; set; }
    public string Email { get; set; }
    public string Site { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string RequiredPosition { get; set; }
    public int Age { get; set; }
    public string Picture { get; set; }
    public string AboutMe { get; set; }
    public string Hobbies { get; set; }
    public List<EducationDocx> Educations { get; set; }
    public List<ExperienceDocx> Experiences { get; set; }
    public List<LanguageDocx> Languages { get; set; }
    public List<SkillDocx> Skills { get; set; }
    public decimal SalaryRate { get; set; }
    public int? CountDaysUnavailable { get; set; }
}

public class EducationDocx
{
    public string InstitutionName { get; set; }
    public string Specialization { get; set; }
    public string Degree { get; set; }
    public string Description { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string CountTime { get; set; }
}

public class ExperienceDocx
{
    public string Company { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string CountTime { get; set; }
    // public List<string> Skills { get; set; }
}

public class LanguageDocx
{
    public string LanguageName { get; set; }
    public string Level { get; set; }
}

public class SkillDocx
{
    public string SkillName { get; set; }
    public string Level { get; set; }
}