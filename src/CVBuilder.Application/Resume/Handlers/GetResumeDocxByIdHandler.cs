using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Services.DocxBuilder;
using MediatR;

namespace CVBuilder.Application.Resume.Handlers;

public class GetResumeDocxByIdHandler : IRequestHandler<GetDocxByIdQuery, Stream>
{
    private readonly IMediator _mediator;
    private readonly DocxBuilderV2 _docxBuilder;

    public GetResumeDocxByIdHandler(IMediator mediator, DocxBuilderV2 docxBuilder)
    {
        _mediator = mediator;
        _docxBuilder = docxBuilder;
    }

    public async Task<Stream> Handle(GetDocxByIdQuery request, CancellationToken cancellationToken)
    {
        var query = new GetResumePdfByIdQuery
        {
            ResumeId = request.ResumeId,
            UserId = request.UserId,
            UserRoles = request.UserRoles
        };

        await using var result = await _mediator.Send(query, cancellationToken);
        var docxStream = await _docxBuilder.ConvertPdfToDocx(result, cancellationToken);

        return docxStream;
    }
}