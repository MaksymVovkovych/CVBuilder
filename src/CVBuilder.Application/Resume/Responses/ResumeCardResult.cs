using System;
using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses.Shared;
using CVBuilder.Application.User.Responses;
using CVBuilder.Models;

namespace CVBuilder.Application.Resume.Responses;

public class ResumeCardResult
{
    public int Id { get; set; }
    public string ResumeName { get; set; }
    public string PositionName { get; set; }
    public bool IsDraft { get; set; }
    public decimal SalaryRate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AboutMe { get; set; }
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public DateInterval? DateInterval { get; set; }
    public DateTime? AvailabilityStatusDate { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<LevelSkillResult> Skills { get; set; }
    public string ShortUrlFullResume { get; set; }
    public string ShortUrlIncognito { get; set; }
    public string ShortUrlIncognitoWithoutLogo { get; set; }
    public List<ResumeClientResult> Clients { get; set; }
    public UserResult CreatedBy { get; set; }
    public UserResult Owner { get; set; }
}