using System.Collections.Generic;
using CVBuilder.Application.Experience.Responses;
using MediatR;

namespace CVBuilder.Application.Experience.Queries;

public class GetAllExperiencesQuery : IRequest<List<ExperienceResult>>
{
}