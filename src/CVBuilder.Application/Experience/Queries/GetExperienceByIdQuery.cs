using CVBuilder.Application.Experience.Responses;
using MediatR;

namespace CVBuilder.Application.Experience.Queries;

public class GetExperienceByIdQuery : IRequest<GetExperienceByIdResult>
{
    public int Id { get; set; }
}