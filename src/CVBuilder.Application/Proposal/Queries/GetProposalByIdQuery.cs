using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetProposalByIdQuery : IRequest<ProposalResult>
{
    public int Id { get; set; }
}