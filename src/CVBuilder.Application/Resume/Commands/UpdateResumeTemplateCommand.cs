using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class UpdateResumeTemplateCommand : IRequest<TemplateResult>
{
    public int TemplateId { get; set; }
    public string TemplateName { get; set; }
    public string Html { get; set; }
    public string EditHtml { get; set; }
}