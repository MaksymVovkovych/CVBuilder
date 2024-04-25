using CVBuilder.Application.Complexity.Result;
using MediatR;

namespace CVBuilder.Application.Complexity.Commands;

public class UpdateComplexityCommand : IRequest<ComplexityResult>
{
    public int Id { get; set; }
    public string ComplexityName { get; set; }
}