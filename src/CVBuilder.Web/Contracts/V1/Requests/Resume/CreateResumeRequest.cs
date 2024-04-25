using System.Collections.Generic;
using CVBuilder.Models;
using CVBuilder.Web.Contracts.V1.Requests.Position;
using CVBuilder.Web.Contracts.V1.Requests.Resume.SharedResumeRequest;

namespace CVBuilder.Web.Contracts.V1.Requests.Resume;

public class CreateResumeRequest
{
    public string ResumeName { get; set; }
    public bool IsDraft { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? ImageId { get; set; }
    public PositionLevel? PositionLevel { get; set; }
    public string Picture { get; set; }
    public PositionRequest Position { get; set; }
    public string Email { get; set; }
    public string Site { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string RequiredPosition { get; set; }
    public string Birthdate { get; set; }
    public string AboutMe { get; set; }
    public string Hobbies { get; set; }
    public int ResumeTemplateId { get; set; }
    public decimal? SalaryRate { get; set; }
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public int? CountDaysUnavailable { get; set; }
    public List<CreateEducationRequest> Educations { get; set; }
    public List<CreateExperienceRequest> Experiences { get; set; }
    public List<CreateSkillRequest> Skills { get; set; }
    public List<CreateLanguageRequest> Languages { get; set; }
}