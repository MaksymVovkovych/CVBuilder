using System.Collections.Generic;

namespace CVBuilder.Application.Resume.Services.ResumeBuilder.ClassFiledParser;

public class ParsedListFields
{
    public string ListName { get; set; }
    public List<Dictionary<string, string>> ListValues { get; set; }
}