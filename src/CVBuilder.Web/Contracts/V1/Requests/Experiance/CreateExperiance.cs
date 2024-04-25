using System;

namespace CVBuilder.Web.Contracts.V1.Requests.Experiance;

public class CreateExperiance
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