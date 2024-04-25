using System;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Models;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Proposal.Mapper;

public class CreateProposalMapper : AppMapperBase
{
    public CreateProposalMapper()
    {
        CreateMap<CreateProposalCommand, Models.Entities.Proposal>()
            .ForMember(x => x.CreatedAt, y => y.MapFrom(z => DateTime.UtcNow))
            .ForMember(x => x.StatusProposal, y => y.MapFrom(z => StatusProposal.Created))
            .ForMember(x => x.UpdatedAt, y => y.MapFrom(z => DateTime.UtcNow))
            .ForMember(x => x.CreatedUserId, y => y.MapFrom(z => z.UserId))
            .ForMember(x => x.ClientId, y => y.MapFrom(z => z.ClientId));
        CreateMap<CreateResumeCommand, ProposalResume>()
            .ForMember(x => x.StatusResume, y => y.MapFrom(z => StatusProposalResume.NotSelected));
    }
}