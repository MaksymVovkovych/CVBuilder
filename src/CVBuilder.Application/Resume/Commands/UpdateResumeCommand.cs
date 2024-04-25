using System.Collections.Generic;
using CVBuilder.Application.Core.Infrastructure;
using CVBuilder.Application.Resume.Commands.SharedCommands;
using CVBuilder.Application.Resume.Responses.Shared;
using CVBuilder.Models;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class UpdateResumeCommand : AuthorizedRequest<ResumeResult>
{
    public int Id { get; set; }
    public string Picture { get; set; }
    public string ResumeName { get; set; }
    public bool IsDraft { get; set; }
    public PositionLevel? PositionLevel { get; set; }
    public int? PositionId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Site { get; set; }
    public string Phone { get; set; }
    public string Code { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string RequiredPosition { get; set; }
    public int ResumeTemplateId { get; set; }
    public string Birthdate { get; set; }
    public decimal? SalaryRate { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }

    public int? CountDaysUnavailable { get; set; }
    public string Hobbies { get; set; }
    public string Timestamp { get; set; }

    // public IFormFile Picture { get; set; }
    public string AboutMe { get; set; }
    public List<UpdateEducationCommand> Educations { get; set; }
    public List<UpdateExperienceCommand> Experiences { get; set; }
    public List<UpdateSkillCommand> Skills { get; set; }
    public List<UpdateLanguageCommand> Languages { get; set; }
}