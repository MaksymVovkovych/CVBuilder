using System.Collections.Generic;
using CVBuilder.Application.Core.Infrastructure;
using CVBuilder.Application.Resume.Responses.Shared;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

using Models.Entities;

public class GetResumeByIdQuery : AuthorizedRequest<ResumeResult>
{
    public int Id { get; set; }
    public bool IsByUrl { get; set; }
}