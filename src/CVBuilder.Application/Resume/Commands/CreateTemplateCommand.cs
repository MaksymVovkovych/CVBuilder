using System.IO;
using CVBuilder.Application.Resume.Responses;
using MediatR;

namespace CVBuilder.Application.Resume.Commands;

public class CreateTemplateCommand : IRequest<TemplateResult>
{
    public string TemplateName { get; set; }
    public Stream HtmlStream { get; set; }
}