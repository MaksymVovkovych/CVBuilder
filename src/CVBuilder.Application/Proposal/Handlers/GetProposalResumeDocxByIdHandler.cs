using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Services.DocxBuilder;
using MediatR;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetProposalResumeDocxByIdHandler : IRequestHandler<GetProposalResumeDocxByIdQuery, Stream>
{
    private readonly IMediator _mediator;
    private readonly DocxBuilderV2 _docxBuilder;

    public GetProposalResumeDocxByIdHandler(IMediator mediator, DocxBuilderV2 docxBuilder)
    {
        _mediator = mediator;
        _docxBuilder = docxBuilder;
    }

    public async Task<Stream> Handle(GetProposalResumeDocxByIdQuery request, CancellationToken cancellationToken)
    {
        var query = new GetProposalResumePdfByIdQuery
        {
            ProposalId = request.ProposalId,
            ProposalResumeId = request.ProposalResumeId,
            UserId = request.UserId,
            UserRoles = request.UserRoles
        };

        await using var pdfStream = await _mediator.Send(query, cancellationToken);

        var docxStream = await _docxBuilder.ConvertPdfToDocx(pdfStream, cancellationToken);

        return docxStream;
    }
}