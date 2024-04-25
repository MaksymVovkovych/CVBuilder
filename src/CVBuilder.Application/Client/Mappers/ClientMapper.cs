using System.Linq;
using CVBuilder.Application.Client.Responses;

namespace CVBuilder.Application.Client.Mappers;

internal class ClientMapper : AppMapperBase
{
    public ClientMapper()
    {
        CreateMap<Models.Entities.User, ClientListItemResponse>()
            .ForMember(
                x => x.Proposals,
                y => y.MapFrom(z => string.Join(", ", z.ClientProposals.Select(p => p.ProposalName).ToList())));

        CreateMap<Models.Entities.User, ClientResponse>();
    }
}