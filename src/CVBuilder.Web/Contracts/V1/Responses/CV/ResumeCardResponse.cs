using System;
using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models;
using CVBuilder.Web.Contracts.V1.Responses.Skill;

namespace CVBuilder.Web.Contracts.V1.Responses.CV;

public class UserResponse
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class ResumeCardResponse
{
    public int Id { get; set; }
    public string ResumeName { get; set; }
    public bool IsDraft { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PositionName { get; set; }
    public decimal SalaryRate { get; set; }
    public string AboutMe { get; set; }
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public DateInterval? DateInterval { get; set; }
    public DateTime? AvailabilityStatusDate { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<SkillResponse> Skills { get; set; }
    public List<ResumeClientResult> Clients { get; set; }
    public string ShortUrlFullResume { get; set; }
    public string ShortUrlIncognito { get; set; }
    public string ShortUrlIncognitoWithoutLogo { get; set; }
    public UserResponse CreatedBy { get; set; }
    public UserResponse Owner { get; set; }
}