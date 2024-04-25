using System.Collections.Generic;

namespace CVBuilder.Application.Resume.Services.Pagination;

public class PagedResponse<T>
{
    public PagedResponse(List<T> items, int? page, int? pageSize, int totalRecords)
    {
        Page = page ?? 1;
        PageSize = pageSize ?? totalRecords;
        TotalRecords = totalRecords;
        Items = items;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public List<T> Items { get; set; }
}