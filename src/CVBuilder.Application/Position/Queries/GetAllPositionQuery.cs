using System.Collections.Generic;
using CVBuilder.Application.Position.Responses;
using MediatR;

namespace CVBuilder.Application.Position.Queries;

public class GetAllPositionQuery : IRequest<List<PositionResult>>
{
}