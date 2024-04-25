using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

public class GetProposalResumeByUrlHandler : IRequestHandler<GetProposalResumeByUrlQuery, ProposalResumeResult>
{
    private readonly IMediator _mediator;
    private readonly IRepository<ProposalResume, int> _proposalResumeRepository;

    public GetProposalResumeByUrlHandler(IRepository<ProposalResume, int> proposalResumeRepository,
        IMediator mediator)
    {
        _proposalResumeRepository = proposalResumeRepository;
        _mediator = mediator;
    }

    public async Task<ProposalResumeResult> Handle(GetProposalResumeByUrlQuery request,
        CancellationToken cancellationToken)
    {
        var resume = await _proposalResumeRepository.Table
            .Include(x => x.ShortUrl)
            .FirstOrDefaultAsync(x => x.ShortUrl.Url == request.ShortUrl, cancellationToken);

        if (resume == null) throw new NotFoundException("Resume not found");

        var command = new GetProposalResumeByIdQuery
        {
            UserRoles = request.UserRoles,
            UserId = request.UserId,
            ProposalId = resume.ProposalId,
            ProposalResumeId = resume.Id
        };

        var result = await _mediator.Send(command, cancellationToken);
       
        return result;
    }
}