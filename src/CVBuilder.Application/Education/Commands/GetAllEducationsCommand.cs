using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses.Shared;
using MediatR;

namespace CVBuilder.Application.Education.Commands;

public class GetAllEducationsCommand : IRequest<List<EducationResult>>
{
}