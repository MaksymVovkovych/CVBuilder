using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Holidays.Commands;
using CVBuilder.Application.Holidays.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Holidays.Handlers;

public class CreateHolidayHandler: IRequestHandler<CreateHolidayCommand,HolidayResult>
{
    private readonly IRepository<Holiday, int> _holidayRepository;
    private readonly IMapper _mapper;

    public CreateHolidayHandler(IRepository<Holiday, int> holidayRepository, IMapper mapper)
    {
        _holidayRepository = holidayRepository;
        _mapper = mapper;
    }

    public async Task<HolidayResult> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
    {
        var holidayDb = await _holidayRepository.Table
            .FirstOrDefaultAsync(x => x.Date.Date == request.Date.Date, cancellationToken: cancellationToken);

        if (holidayDb != null)
            throw new ConflictException("Holiday with same date exists");

        var newHoliday = _mapper.Map<Holiday>(request);
        newHoliday = await _holidayRepository.CreateAsync(newHoliday);

        var holiday = _mapper.Map<HolidayResult>(newHoliday);

        return holiday;
    }
}