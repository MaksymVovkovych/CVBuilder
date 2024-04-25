using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Holidays.Queries;
using CVBuilder.Application.Holidays.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenXmlPowerTools;

namespace CVBuilder.Application.Holidays.Handlers;

public class GetAllHolidaysHandler: IRequestHandler<GetAllHolidaysQuery,List<HolidayResult>>
{
    private readonly IRepository<Holiday, int> _holidaysRepository;
    private readonly IMapper _mapper;
    
    
    public GetAllHolidaysHandler(IRepository<Holiday, int> holidaysRepository, IMapper mapper)
    {
        _holidaysRepository = holidaysRepository;
        _mapper = mapper;
    }

    public async Task<List<HolidayResult>> Handle(GetAllHolidaysQuery request, CancellationToken cancellationToken)
    {
        var holidaysDb = await _holidaysRepository.Table
            .OrderBy(x=>x.Date)
            .ToListAsync(cancellationToken: cancellationToken);

        var holidays = _mapper.Map<List<HolidayResult>>(holidaysDb);

        return holidays;
    }
}