using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetDocxTemplateByIdQueries : IRequest<byte[]>
{
    public GetDocxTemplateByIdQueries(int templateId)
    {
        TemplateId = templateId;
    }

    public int TemplateId { get; }
}