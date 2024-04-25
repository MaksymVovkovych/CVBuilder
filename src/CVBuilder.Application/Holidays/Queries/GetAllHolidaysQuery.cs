using System.Collections.Generic;
using CVBuilder.Application.Holidays.Responses;
using MediatR;

namespace CVBuilder.Application.Holidays.Queries;

public class GetAllHolidaysQuery: IRequest<List<HolidayResult>>
{
    
}