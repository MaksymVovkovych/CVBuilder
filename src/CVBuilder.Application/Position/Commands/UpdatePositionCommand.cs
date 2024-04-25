using CVBuilder.Application.Position.Responses;
using MediatR;

namespace CVBuilder.Application.Position.Commands;

public class UpdatePositionCommand : IRequest<PositionResult>
{
    public int PositionId { get; set; }
    public string PositionName { get; set; }
}