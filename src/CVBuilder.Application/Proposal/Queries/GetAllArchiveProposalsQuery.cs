using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetAllArchiveProposalsQuery : IRequest<(int, List<SmallProposalResult>)>
{
    public string Term { get; set; }
    public List<int> Clients { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Sort { get; set; }
    public string Order { get; set; }
}