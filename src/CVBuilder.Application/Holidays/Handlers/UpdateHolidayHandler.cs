using System;
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

public class UpdateHolidayHandler: IRequestHandler<UpdateHolidayCommand, HolidayResult>
{
    private readonly IRepository<Holiday, int> _holidayRepository;
    private readonly IMapper _mapper;

    public UpdateHolidayHandler(IRepository<Holiday, int> holidayRepository, IMapper mapper)
    {
        _holidayRepository = holidayRepository;
        _mapper = mapper;
    }

    public async Task<HolidayResult> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
    {
        var holidayDb = await _holidayRepository.Table
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (holidayDb == null)
            throw new NotFoundException("Holiday not found");

        var holidayExists = await _holidayRepository.Table.AnyAsync(x => x.Date.Date == request.Date.Date,
            cancellationToken: cancellationToken);
        
        if(holidayExists)
            throw new ConflictException("Holiday with same date exists");

        holidayDb.Date = request.Date.Date;
        holidayDb.HolidayName = request.HolidayName;
        holidayDb.UpdatedAt = DateTime.UtcNow.Date;

        holidayDb = await _holidayRepository.UpdateAsync(holidayDb);

        var holiday = _mapper.Map<HolidayResult>(holidayDb);

        return holiday;

    }
}