using System;
using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Requests.Resume.SharedResumeRequest;

public class CreateExperienceRequest
{
    public string Company { get; set; }
    public string Position { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<ExperienceSkillRequest> Skills { get; set; }

}