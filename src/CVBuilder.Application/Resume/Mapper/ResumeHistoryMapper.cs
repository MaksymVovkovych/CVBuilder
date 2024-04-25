using CVBuilder.Application.Resume.Responses;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Resume.Mapper;

public class ResumeHistoryMapper: AppMapperBase
{
    public ResumeHistoryMapper()
    {
        CreateMap<ResumeHistory, ResumeHistoryResult>()
            .ForMember(x => x.UpdatedUser, y => y.MapFrom(z => z.UpdateUser))
            .ForMember(x=>x.UpdatedAt,y=>y.MapFrom(z=>z.UpdatedAt.ToString("MM/dd/yyyy HH:mm:ss UTC")));
    }
}