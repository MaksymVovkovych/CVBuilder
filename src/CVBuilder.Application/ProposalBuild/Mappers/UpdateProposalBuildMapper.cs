using CVBuilder.Application.ProposalBuild.Commands;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.ProposalBuild.Mappers;

public class UpdateProposalBuildMapper : AppMapperBase
{
    public UpdateProposalBuildMapper()
    {
        CreateMap<UpdateProposalBuildCommand, Models.Entities.ProposalBuild>()
            .ForMember(x => x.Id, y => y.MapFrom(x => x.Id));
        CreateMap<UpdateProposalBuildPositionCommand, ProposalBuildPosition>();
    }
}