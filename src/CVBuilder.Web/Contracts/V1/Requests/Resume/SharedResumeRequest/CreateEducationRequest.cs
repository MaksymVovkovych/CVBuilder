﻿using System;

namespace CVBuilder.Web.Contracts.V1.Requests.Resume.SharedResumeRequest;

public class CreateEducationRequest
{
    public string InstitutionName { get; set; }
    public string Specialization { get; set; }
    public string Degree { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}