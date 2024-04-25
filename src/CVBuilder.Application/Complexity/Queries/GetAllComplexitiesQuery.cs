using System.Collections.Generic;
using CVBuilder.Application.Complexity.Result;
using MediatR;

namespace CVBuilder.Application.Complexity.Queries;

public class GetAllComplexitiesQuery : IRequest<List<ComplexityResult>>
{
}