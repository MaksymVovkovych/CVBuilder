using CVBuilder.Application.Resume.Services.Pagination;

namespace CVBuilder.Web.Contracts.V1.Requests.User;

public class GetAllUsersRequest : PaginationFilter
{
    public GetAllUsersRequest()
    {
    }

    public GetAllUsersRequest(int? page, int? pageSize, string term) : base(page, pageSize)
    {
        Term = term;
    }

    public string Term { get; set; }
}