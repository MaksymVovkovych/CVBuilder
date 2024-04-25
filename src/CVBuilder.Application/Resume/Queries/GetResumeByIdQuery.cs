using CVBuilder.Application.Core.Infrastructure;
using CVBuilder.Application.Resume.Responses.Shared;

namespace CVBuilder.Application.Resume.Queries;

public class GetResumeByIdQuery : AuthorizedRequest<ResumeResult>
{
    public int Id { get; set; }
    public bool IsByUrl { get; set; }
}