using CVBuilder.Application.Resume.Services.Pagination;

namespace CVBuilder.Web.Contracts.V1.Requests.Client;

public class GetAllClientsRequest : PaginationFilter
{
    public GetAllClientsRequest()
    {
    }

    public GetAllClientsRequest(int? page, int? pageSize, string term) : base(page, pageSize)
    {
        Term = term;
    }

    public string Term { get; set; }
}