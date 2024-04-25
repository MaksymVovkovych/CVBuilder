using System.Collections.Generic;
using System.IO;
using CVBuilder.Application.Core.Infrastructure;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetDocxByIdQuery : AuthorizedRequest<Stream>
{
    public int ResumeId { get; set; }

}