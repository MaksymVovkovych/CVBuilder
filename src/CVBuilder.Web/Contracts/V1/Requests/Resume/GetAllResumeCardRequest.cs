using System.Collections.Generic;
using CVBuilder.Application.Resume.Services.Pagination;

namespace CVBuilder.Web.Contracts.V1.Requests.Resume;

public class GetAllResumeCardRequest : PaginationFilter
{
    public GetAllResumeCardRequest()
    {
    }

    public GetAllResumeCardRequest(int? page, int? pageSize, string term, List<int> positions, List<int> skills,
        List<int> clients, List<int> statuses, List<int> users) : base(page, pageSize)
    {
        Term = term;
        Positions = positions;
        Skills = skills;
        Clients = clients;
        Statuses = statuses;
        Users = users;
    }

    public string Term { get; set; }

    public List<int> Positions { get; set; }

    public List<int> Skills { get; set; }

    public List<int> Clients { get; set; }
    public List<int> Statuses { get; set; }
    public List<int> Users { get; set; }

    public bool IsArchive { get; set; }
}