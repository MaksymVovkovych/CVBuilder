using System.Collections.Generic;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Application.Resume.Services.Pagination;
using MediatR;

namespace CVBuilder.Application.Resume.Queries;

public class GetAllResumeCardQuery : PaginationFilter, IRequest<(int, List<ResumeCardResult>)>
{
    public int UserId { get; set; }
    public IEnumerable<string> UserRoles { get; set; }
    public string Term { get; set; }
    public List<int> Positions { get; set; }
    public List<int> Skills { get; set; }
    public List<int> Clients { get; set; }
    public List<int> Statuses { get; set; }
    public List<int> Users { get; set; }
    public bool IsArchive { get; set; }
}