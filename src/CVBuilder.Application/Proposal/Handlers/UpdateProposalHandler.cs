using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;

public class UpdateProposalHandler : IRequestHandler<UpdateProposalCommand, ProposalResult>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRepository<Proposal, int> _proposalRepository;
    private readonly IShortUrlService _shortUrlService;

    public UpdateProposalHandler(IMapper mapper, IRepository<Proposal, int> proposalRepository,
        IMediator mediator,
        IShortUrlService shortUrlService)
    {
        _mapper = mapper;
        _proposalRepository = proposalRepository;
        _mediator = mediator;
        _shortUrlService = shortUrlService;
    }

    public async Task<ProposalResult> Handle(UpdateProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = _mapper.Map<Proposal>(request);
        var proposalDb = await _proposalRepository.Table
            .Include(x => x.Resumes)
            .ThenInclude(x => x.ShortUrl)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (proposalDb == null) throw new NotFoundException("Proposal not found");

        MapResumes(proposalDb, request);
        UpdateProposal(proposalDb, proposal);
        RemoveDuplicate(proposalDb);
        proposalDb = await _proposalRepository.UpdateAsync(proposalDb);
        var result = await _mediator.Send(new GetProposalByIdQuery { Id = proposalDb.Id }, cancellationToken);
        return result;
    }

    private void MapResumes(Proposal proposalDb, UpdateProposalCommand proposal)
    {
        var proposalResumes = new List<ProposalResume>();
        foreach (var resumeRequest in proposal.Resumes)
        {
            if (!resumeRequest.Id.HasValue)
            {
                proposalResumes.Add(new ProposalResume
                {
                    ResumeId = resumeRequest.ResumeId,
                    ShortUrl = new ShortUrl
                    {
                        Url = _shortUrlService.GenerateShortUrl()

                    }
                });
                continue;
            }


            var resumeDb = proposalDb.Resumes.FirstOrDefault(x => x.Id == resumeRequest.Id);
            if (resumeDb != null)
                proposalResumes.Add(resumeDb);
            else if (!resumeRequest.Id.HasValue)
                proposalDb.Resumes.Add(new ProposalResume
                {
                    ResumeId = resumeRequest.ResumeId,
                    ShortUrl = new ShortUrl
                    {
                        Url = _shortUrlService.GenerateShortUrl()

                    }
                });
            
        }

        proposalDb.Resumes = proposalResumes;
    }

    private static void RemoveDuplicate(Proposal proposalDto)
    {
        proposalDto.Resumes = proposalDto.Resumes
            .GroupBy(x => x.ResumeId)
            .Select(y => y.First())
            .ToList();
    }


    private static void UpdateProposal(Proposal proposalDb, Proposal proposal)
    {
        proposalDb.UpdatedAt = DateTime.UtcNow;
        proposalDb.ResumeTemplateId = proposal.ResumeTemplateId == 0 ? 1 : proposal.ResumeTemplateId;
        proposalDb.IsIncognito = proposal.IsIncognito;
        proposalDb.ShowLogo = proposal.ShowLogo;
        proposalDb.ShowCompanyNames = proposal.ShowCompanyNames;
        proposalDb.StatusProposal = proposal.StatusProposal;
        proposalDb.ProposalName = proposal.ProposalName;
        proposalDb.ClientId = proposal.ClientId;
    }
}