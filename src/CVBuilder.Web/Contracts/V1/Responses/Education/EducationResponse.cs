using System;

namespace CVBuilder.Web.Contracts.V1.Responses.Education;

public class EducationResponse
{
    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Degree { get; set; }
    public string Description { get; set; }
}