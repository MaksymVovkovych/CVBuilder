using System;
using System.Collections.Generic;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Application.User.Responses;
using CVBuilder.Models;

namespace CVBuilder.Application.Resume.Responses.Shared;

public class ResumeResult
{
    public int Id { get; set; }
    public string ResumeName { get; set; }
    public bool IsDraft { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public PositionResult Position { get; set; }
    public PositionLevel? PositionLevel { get; set; }
    public string Email { get; set; }
    public string Site { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string RequiredPosition { get; set; }
    public int ResumeTemplateId { get; set; }
    public DateTime Birthdate { get; set; }
    public string Picture { get; set; }
    public string AboutMe { get; set; }
    public string Hobbies { get; set; }
    public List<EducationResult> Educations { get; set; }
    public List<ExperienceResult> Experiences { get; set; }
    public List<LevelLanguageResult> Languages { get; set; }
    public List<LevelSkillResult> Skills { get; set; }
    public decimal SalaryRate { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
    public int? CountDaysUnavailable { get; set; }
    public string ShortUrlFullResume { get; set; }
    public string ShortUrlIncognito { get; set; }
    public string ShortUrlIncognitoWithoutLogo { get; set; }
    public DateTime? DeletedAt { get; set; }
    public UserResult CreatedBy { get; set; }
    public UserResult Owner { get; set; }
    public string Timestamp { get; set; }

}