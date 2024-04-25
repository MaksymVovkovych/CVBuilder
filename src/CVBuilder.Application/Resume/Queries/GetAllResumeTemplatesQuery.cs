using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetAllResumeTemplatesQuery : IRequest<List<ResumeTemplateCardResult>>
{
}