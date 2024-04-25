using CVBuilder.Application.Education.Responses;
using MediatR;

namespace CVBuilder.Application.Education.Commands;

public class GetEducationByIdCommand : IRequest<EducationByIdResult>
{
    public int Id { get; set; }
}