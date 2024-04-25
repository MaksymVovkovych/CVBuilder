using System.Collections.Generic;
using CVBuilder.Application.Resume.Services.Pagination;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class GetAllProposalsRequest : PaginationFilter
{
    public GetAllProposalsRequest()
    {
    }

    public GetAllProposalsRequest(int? page, int? pageSize, string term, List<int> clients, List<int> statuses) :
        base(page, pageSize)
    {
        Term = term;
        Clients = clients;
        Statuses = statuses;
    }

    public string Term { get; set; }
    public List<int> Clients { get; set; }
    public List<int> Statuses { get; set; }
}