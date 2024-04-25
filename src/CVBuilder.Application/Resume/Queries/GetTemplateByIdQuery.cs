using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetTemplateByIdQuery : IRequest<TemplateResult>
{
    public int Id { get; set; }
}