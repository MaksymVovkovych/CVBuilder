using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Repository;
using MediatR;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CVBuilder.Application.Resume.Handlers;

using Models.Entities;

public class GetResumePdfByUrlHandler : IRequestHandler<GetResumePdfByUrlQuery, Stream>
{
    private readonly AppSettings _appSettings;
    private readonly IBrowserPdfPrinter _browserPdfPrinter;
    private readonly IMediator _mediator;
    private readonly IRepository<ResumeTemplate, int> _templateRepository;


    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    public GetResumePdfByUrlHandler(IBrowserPdfPrinter browserPdfPrinter, AppSettings appSettings, IMediator mediator,
        IRepository<ResumeTemplate, int> templateRepository)
    {
        _browserPdfPrinter = browserPdfPrinter;
        _appSettings = appSettings;
        _mediator = mediator;
        _templateRepository = templateRepository;
    }

    public async Task<Stream> Handle(GetResumePdfByUrlQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetResumeByUrlQuery
        {
            Url = request.Url,
        }, cancellationToken);

        var json = JsonSerializer.Serialize(result, _jsonSerializerOptions);

        var template = await _mediator.Send(new GetTemplateByIdQuery
        {
            Id = result.Resume.ResumeTemplateId
        }, cancellationToken);


        var templateHtml = JsonSerializer.Serialize(template, _jsonSerializerOptions);


        await using var page =
            await _browserPdfPrinter
                .LoadPageAsync($"{_appSettings.FrontendUrl}/resume/download", json, templateHtml);

        var stream = await _browserPdfPrinter.PrintPdfAsync();
        return stream;
    }
}