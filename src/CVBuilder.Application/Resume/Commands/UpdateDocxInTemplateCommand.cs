using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class UpdateDocxInTemplateCommand : IRequest<TemplateResult>
{
    public UpdateDocxInTemplateCommand(int templateId, byte[] data)
    {
        TemplateId = templateId;
        Data = data;
    }

    public int TemplateId { get; }
    public byte[] Data { get; }
}