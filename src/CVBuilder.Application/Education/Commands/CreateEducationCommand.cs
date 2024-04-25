using System;
using CVBuilder.Application.Education.Responses;
using MediatR;

namespace CVBuilder.Application.Education.Commands;

public class CreateEducationCommand : IRequest<CreateEducationResult>
{
    public int CvId { get; set; }
    public string InstitutionName { get; set; }
    public string Specialization { get; set; }
    public string Degree { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}