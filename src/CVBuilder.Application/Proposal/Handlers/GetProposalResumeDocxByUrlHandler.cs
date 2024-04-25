using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Services.DocxBuilder;
using MediatR;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetProposalResumeDocxByUrlHandler : IRequestHandler<GetProposalResumeDocxByUrlQuery, Stream>
{
    private readonly IMediator _mediator;
    private readonly DocxBuilderV2 _docxBuilder;

    public GetProposalResumeDocxByUrlHandler(IMediator mediator, DocxBuilderV2 docxBuilder)
    {
        _mediator = mediator;
        _docxBuilder = docxBuilder;
    }

    public async Task<Stream> Handle(GetProposalResumeDocxByUrlQuery request, CancellationToken cancellationToken)
    {
        var query = new GetProposalResumePdfByUrlQuery
        {
            ShortUrl = request.ShortUrl,
            UserId = request.UserId,
            UserRoles = request.UserRoles
        };

        await using var pdfStream = await _mediator.Send(query, cancellationToken);

        var docxStream = await _docxBuilder.ConvertPdfToDocx(pdfStream, cancellationToken);

        return docxStream;
    }
}