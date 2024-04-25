using MediatR;

namespace CVBuilder.Application.Complexity.Commands;

public class DeleteComplexityCommand : IRequest<bool>
{
    public int Id { get; set; }
}