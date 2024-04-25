using System;
using CVBuilder.Application.Experience.Responses;
using MediatR;

namespace CVBuilder.Application.Experience.Commands;

public class CreateExperienceCommand : IRequest<CreateExperienceResult>
{
    public int CvId { get; set; }
    public string Company { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}