using CVBuilder.Application.Resume.Responses.Shared;

namespace CVBuilder.Application.Resume.Responses;

public class ResumeShortUrlResult
{
    public bool ShowLogo { get; set; }
    public ResumeResult Resume { get; set; }
}