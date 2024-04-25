using System.Collections.Generic;
using CVBuilder.Application.Resume.Commands.SharedCommands;
using CVBuilder.Application.Resume.Responses.Shared;
using CVBuilder.Models;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class CreateResumeCommand : IRequest<ResumeResult>
{
    public int UserId { get; set; }
    public string ResumeName { get; set; }
    public bool IsDraft { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? ImageId { get; set; }
    public PositionLevel? PositionLevel { get; set; }
    public string Picture { get; set; }
    public int PositionId { get; set; }
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
    public AvailabilityStatus AvailabilityStatus { get; set; }
    public int? CountDaysUnavailable { get; set; }
    public List<CreateEducationCommand> Educations { get; set; }
    public List<CreateExperienceCommand> Experiences { get; set; }
    public List<CreateSkillCommand> Skills { get; set; }
    public List<CreateLanguageCommand> Languages { get; set; }
    public List<string> UserRoles { get; set; }
}