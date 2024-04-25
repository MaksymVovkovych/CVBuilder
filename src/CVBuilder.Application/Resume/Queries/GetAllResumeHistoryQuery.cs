using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetAllResumeHistoryQuery: IRequest<List<ResumeHistoryResult>>
{
    public int ResumeId { get; set; }
}