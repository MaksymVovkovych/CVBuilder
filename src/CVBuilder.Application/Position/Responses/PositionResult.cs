using MediatR;

namespace CVBuilder.Application.Position.Responses;

public class PositionResult : IRequest<bool>
{
    public int PositionId { get; set; }
    public string PositionName { get; set; }
}