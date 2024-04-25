using CVBuilder.Application.Client.Commands;
using CVBuilder.Application.Client.Queries;
using CVBuilder.Web.Contracts.V1.Requests.Client;

namespace CVBuilder.Web.Mappers;

public class ClientMapper : MapperBase
{
    public ClientMapper()
    {
        CreateMap<GetAllClientsRequest, GetAllClientsQueries>();

        #region CreateClient

        CreateMap<CreateClientRequest, CreateClientCommand>();

        #endregion

        #region UpdateClient

        CreateMap<UpdateClientRequest, UpdateClientCommand>();

        #endregion
    }
}