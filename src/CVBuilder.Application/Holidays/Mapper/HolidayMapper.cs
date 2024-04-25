using CVBuilder.Application.Holidays.Commands;
using CVBuilder.Application.Holidays.Responses;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Holidays.Mapper;

public class HolidayMapper: AppMapperBase
{
    public HolidayMapper()
    {
        CreateMap<Holiday, HolidayResult>();
        CreateMap<CreateHolidayCommand, Holiday>()
            .ForMember(x=>x.Date,y=>y.MapFrom(z=>z.Date.Date));

        CreateMap<Holiday, Holiday>()
            .ForMember(x => x.Date, y => y.MapFrom(z => z.Date.Date));
    }
}