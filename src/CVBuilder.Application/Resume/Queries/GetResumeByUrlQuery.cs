using System.Collections.Generic;
using CVBuilder.Application.Core.Infrastructure;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetResumeByUrlQuery : AuthorizedRequest<ResumeShortUrlResult>
{
    public string Url { get; set; }
}