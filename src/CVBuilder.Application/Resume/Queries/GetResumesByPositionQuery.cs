using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetResumesByPositionQuery : IRequest<List<ResumeCardResult>>
{
    public List<string> Positions { get; set; }
}