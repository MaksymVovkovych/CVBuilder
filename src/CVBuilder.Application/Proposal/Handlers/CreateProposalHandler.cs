using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;

public class ProposalCreateHandler : IRequestHandler<CreateProposalCommand, ProposalResult>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<Proposal, int> _proposalRepository;
    private readonly IShortUrlService _shortUrlService;

    public ProposalCreateHandler(IMapper mapper, IRepository<Proposal, int> proposalRepository,
        IMediator mediator,
        IShortUrlService shortUrlService)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
        _mediator = mediator;
        _shortUrlService = shortUrlService;
    }

    public async Task<ProposalResult> Handle(CreateProposalCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Proposal>(request);
        model.ResumeTemplateId = model.ResumeTemplateId == 0 ? 1 : model.ResumeTemplateId;
        
        foreach (var resume in model.Resumes)
            resume.ShortUrl = new ShortUrl
            {
                Url = _shortUrlService.GenerateShortUrl()
            };

        model = await _proposalRepository.CreateAsync(model);
        var result = await _mediator.Send(new GetProposalByIdQuery { Id = model.Id }, cancellationToken);
        return result;
    }
}