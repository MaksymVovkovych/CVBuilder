using CVBuilder.Application.Core.Infrastructure;
using CVBuilder.Application.Resume.Responses;

namespace CVBuilder.Application.Resume.Queries;

public class GetResumeByUrlQuery : AuthorizedRequest<ResumeShortUrlResult>
{
    public string Url { get; set; }
}