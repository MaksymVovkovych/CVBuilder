using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Web.Contracts.V1.Requests.Proposal;

namespace CVBuilder.Web.Mappers;

public class ProposalMapper : MapperBase
{
    public ProposalMapper()
    {
        CreateMap<CreateProposalRequest, CreateProposalCommand>();
        CreateMap<CreateResumeRequest, CreateResumeCommand>();
        CreateMap<UpdateProposalRequest, UpdateProposalCommand>();
        CreateMap<UpdateResumeRequest, UpdateResumeCommand>();
        CreateMap<ApproveProposalRequest, ApproveProposalCommand>();
        CreateMap<ApproveProposalResumeRequest, ApproveProposalResumeCommand>();
        CreateMap<GetAllProposalsRequest, GetAllProposalsQuery>();
        CreateMap<GetAllProposalsRequest, GetAllArchiveProposalsQuery>();
        CreateMap<RecoverProposalRequest, RecoverProposalRequest>();


        CreateMap<CreateTimePlanningRequest, CreateTimePlanningCommand>();
        CreateMap<GetProposalsCalendarRequest, GetProposalsCalendarQuery>();

        CreateMap<GetProposalsConsumptionRequest, GetProposalsConsumptionQuery>();

        CreateMap<CreateExpenseRequest, CreateExpenseCommand>();
        CreateMap<UpdateExpenseRequest, UpdateExpenseCommand>();
        CreateMap<DuplicateExpensesRequest, DuplicateExpensesCommand>();
    }
}