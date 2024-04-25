using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Services.Interfaces;
using MediatR;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetProposalResumePdfHandler : IRequestHandler<GetProposalResumePdfByIdQuery, Stream>
{
    private readonly AppSettings _appSettings;
    private readonly IBrowserPdfPrinter _browserPdfPrinter;
    private readonly IMediator _mediator;
    
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    public GetProposalResumePdfHandler(IBrowserPdfPrinter browserPdfPrinter, AppSettings appSettings, IMediator mediator)
    {
        _browserPdfPrinter = browserPdfPrinter;
        _appSettings = appSettings;
        _mediator = mediator;
    }

    public async Task<Stream> Handle(GetProposalResumePdfByIdQuery request, CancellationToken cancellationToken)
    {
        
        var result = await _mediator.Send(new GetProposalResumeByIdQuery
        {
            ProposalId = request.ProposalId,
            ProposalResumeId = request.ProposalResumeId,
            UserId = request.UserId,
            UserRoles = request.UserRoles
        }, cancellationToken);

        var json = JsonSerializer.Serialize(result,_jsonSerializerOptions);
     
        var template = await _mediator.Send(new GetTemplateByIdQuery
        {
            Id = result.ResumeTemplateId
        }, cancellationToken);


        var templateHtml = JsonSerializer.Serialize(template,_jsonSerializerOptions);
        
        await using var page =
            await _browserPdfPrinter
                .LoadPageAsync($"{_appSettings.FrontendUrl}/resume/download", json, templateHtml);
        var stream = await _browserPdfPrinter.PrintPdfAsync();
        return stream;
    }
}