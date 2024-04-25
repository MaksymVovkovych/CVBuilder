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

namespace CVBuilder.Application.Resume.Handlers;
using Models.Entities;
public class GetResumePdfByIdHandler : IRequestHandler<GetResumePdfByIdQuery, Stream>
{
    private readonly AppSettings _appSettings;
    private readonly IBrowserPdfPrinter _browserPdfPrinter;
    private readonly IMediator _mediator;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };
    public GetResumePdfByIdHandler(IBrowserPdfPrinter browserPdfPrinter,
        AppSettings appSettings, 
        IMediator mediator)
    {
        _browserPdfPrinter = browserPdfPrinter;
        _appSettings = appSettings;
        _mediator = mediator;
    }

    public async Task<Stream> Handle(GetResumePdfByIdQuery request, CancellationToken cancellationToken)
    {

        var resumeResult = await _mediator.Send(new GetResumeByIdQuery
        {
            Id = request.ResumeId,
            UserId = request.UserId,
            UserRoles = request.UserRoles
        }, cancellationToken);

        var json = JsonSerializer.Serialize(resumeResult,_jsonSerializerOptions);
       
        var template = await _mediator.Send(new GetTemplateByIdQuery
        {
            Id = resumeResult.ResumeTemplateId
        }, cancellationToken);


        var templateHtml = JsonSerializer.Serialize(template,_jsonSerializerOptions);
        
        await using var page =
            await _browserPdfPrinter
                .LoadPageAsync($"{_appSettings.FrontendUrl}/resume/download", json,templateHtml);

        var stream = await _browserPdfPrinter.PrintPdfAsync();
        return stream;
    }
}