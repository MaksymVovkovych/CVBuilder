using CVBuilder.Application.Position.Responses;
using MediatR;

namespace CVBuilder.Application.Position.Commands;

public class CreatePositionCommand : IRequest<PositionResult>
{
    public string PositionName { get; set; }
}