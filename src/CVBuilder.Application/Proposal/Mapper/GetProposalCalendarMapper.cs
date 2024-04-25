using System.Linq;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Application.Proposal.Responses;

namespace CVBuilder.Application.Proposal.Mapper;

using Models.Entities;

public class ResultProposalCalendarMapper: AppMapperBase
{
    public ResultProposalCalendarMapper()
    {
        CreateMap<Proposal, ProposalCalendarResult>()
            .ForMember(x => x.ProposalId, opt => opt.MapFrom(x => x.Id));

        CreateMap<ProposalResume, ProposalResumeCalendarResult>()
            .ForMember(x => x.ProposalResumeId, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Resume.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Resume.LastName))
            .ForMember(x => x.Position, opt => opt.MapFrom(x => x.Resume.Position))
            .ForMember(x => x.SummaryHours, opt => opt.MapFrom(x => x.WorkDays.Sum(y => y.CountHours)));

        CreateMap<Position, PositionResult>()
            .ForMember(x => x.PositionId, opt => opt.MapFrom(x => x.Id));


        CreateMap<WorkDay, WorkDayResult>()
            .ForMember(x => x.Date, opt => opt.MapFrom(y=>y.Date.Date));

    }

}