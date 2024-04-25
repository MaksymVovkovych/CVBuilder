using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetProposalResumePdfByUrlHandler : IRequestHandler<GetProposalResumePdfByUrlQuery, Stream>
{
    private readonly AppSettings _appSettings;
    private readonly IMediator _mediator;
    private readonly IBrowserPdfPrinter _browserPdfPrinter;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public GetProposalResumePdfByUrlHandler(IMediator mediator, IBrowserPdfPrinter browserPdfPrinter,
        AppSettings appSettings)
    {
        _mediator = mediator;
        _browserPdfPrinter = browserPdfPrinter;
        _appSettings = appSettings;
    }

    public async Task<Stream> Handle(GetProposalResumePdfByUrlQuery request, CancellationToken cancellationToken)
    {
        var query = new GetProposalResumeByUrlQuery
        {
            ShortUrl = request.ShortUrl,
            UserId = request.UserId,
            UserRoles = request.UserRoles
        };

        var proposalResume = await _mediator.Send(query, cancellationToken);

        var json = JsonSerializer.Serialize(proposalResume, _jsonSerializerOptions);
        
        var template = await _mediator.Send(new GetTemplateByIdQuery
        {
            Id = proposalResume.ResumeTemplateId
        }, cancellationToken);


        var templateHtml = JsonSerializer.Serialize(template, _jsonSerializerOptions);

        await using var page =
            await _browserPdfPrinter
                .LoadPageAsync($"{_appSettings.FrontendUrl}/resume/download", json, templateHtml);
        var stream = await _browserPdfPrinter.PrintPdfAsync();
        
        return stream;
    }
}