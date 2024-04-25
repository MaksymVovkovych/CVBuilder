using MediatR;

namespace CVBuilder.Application.Position.Commands;

public class DeletePositionCommand : IRequest<bool>
{
    public int PositionId { get; set; }
}