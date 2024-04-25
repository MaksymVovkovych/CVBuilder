namespace CVBuilder.Application.Resume.Services.Pagination;

public class PaginationFilter
{
    protected PaginationFilter()
    {
    }


    protected PaginationFilter(int? page, int? pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string Sort { get; set; }
    public string Order { get; set; }
}