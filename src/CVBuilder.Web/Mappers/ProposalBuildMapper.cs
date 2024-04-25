using CVBuilder.Application.ProposalBuild.Commands;
using CVBuilder.Web.Contracts.V1.Requests.ProposalBuild;

namespace CVBuilder.Web.Mappers;

public class ProposalBuildMapper : MapperBase
{
    public ProposalBuildMapper()
    {
        CreateMap<CreateProposalBuildRequest, CreateProposalBuildCommand>();
        CreateMap<CreateProposalBuildPositionRequest, CreateProposalBuildPositionCommand>();

        CreateMap<UpdateProposalBuildRequest, UpdateProposalBuildCommand>();
        CreateMap<UpdateProposalBuildPositionRequest, UpdateProposalBuildPositionCommand>();
    }
}