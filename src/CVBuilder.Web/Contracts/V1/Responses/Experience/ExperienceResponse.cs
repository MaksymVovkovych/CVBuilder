using System;

namespace CVBuilder.Web.Contracts.V1.Responses.Experience;

public class ExperienceResponse
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
}