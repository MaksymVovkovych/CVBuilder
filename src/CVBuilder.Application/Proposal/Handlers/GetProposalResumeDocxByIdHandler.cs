using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses.Docx;
using CVBuilder.Application.Resume.Services.DocxBuilder;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;

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