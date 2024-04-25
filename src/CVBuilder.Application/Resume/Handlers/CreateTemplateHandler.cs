using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers;

public class CreateTemplateHandler : IRequestHandler<CreateTemplateCommand, TemplateResult>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ResumeTemplate, int> _templateRepository;

    public CreateTemplateHandler(IRepository<ResumeTemplate, int> templateRepository, IMapper mapper)
    {
        _templateRepository = templateRepository;
        _mapper = mapper;
    }

    public async Task<TemplateResult> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
    {
        string html;
        using var reader = new StreamReader(request.HtmlStream, Encoding.UTF8);
        {
            html = await reader.ReadToEndAsync();
        }

        var template = new ResumeTemplate
        {
            TemplateName = request.TemplateName,
            Html = html
        };
        template = await _templateRepository.CreateAsync(template);

        var model = _mapper.Map<TemplateResult>(template);
        return model;
    }
}