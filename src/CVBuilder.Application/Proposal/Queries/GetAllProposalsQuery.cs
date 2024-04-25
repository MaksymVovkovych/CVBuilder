using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetAllProposalsQuery : IRequest<(int, List<SmallProposalResult>)>
{
    public int UserId { get; set; }
    public List<string> UserRoles { get; set; }
    public string Term { get; set; }
    public List<int> Clients { get; set; }
    public List<int> Statuses { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public string Sort { get; set; }
    public string Order { get; set; }
}