using CVBuilder.Application.Complexity.Result;
using MediatR;

namespace CVBuilder.Application.Complexity.Commands;

public class CreateComplexityCommand : IRequest<ComplexityResult>
{
    public string ComplexityName { get; set; }
}