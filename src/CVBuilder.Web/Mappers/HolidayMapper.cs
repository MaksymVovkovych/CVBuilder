using CVBuilder.Application.Holidays.Commands;
using CVBuilder.Web.Contracts.V1.Requests.Holiday;

namespace CVBuilder.Web.Mappers;

public class HolidayMapper : MapperBase
{
    public HolidayMapper()
    {
        CreateMap<CreateHolidayRequest, CreateHolidayCommand>()
            .ForMember(x => x.Date, y => y.MapFrom(z => z.Date.Date));

        CreateMap<UpdateHolidayRequest, UpdateHolidayCommand>()
            .ForMember(x => x.Date, y => y.MapFrom(z => z.Date.Date));

    }
}