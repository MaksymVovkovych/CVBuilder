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

public class DeleteHolidayHandler: IRequestHandler<DeleteHolidayCommand, HolidayResult>
{
    private readonly IRepository<Holiday, int> _holidayRepository;
    private readonly IMapper _mapper;

    public DeleteHolidayHandler(IRepository<Holiday, int> holidayRepository, IMapper mapper)
    {
        _holidayRepository = holidayRepository;
        _mapper = mapper;
    }

    public async Task<HolidayResult> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
    {
        var holidayDb = await _holidayRepository.Table
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (holidayDb == null)
            throw new NotFoundException("Holiday not found");
        
        holidayDb.DeletedAt = DateTime.UtcNow;
        holidayDb.UpdatedAt = DateTime.UtcNow;

        holidayDb = await _holidayRepository.UpdateAsync(holidayDb);

        var holiday = _mapper.Map<HolidayResult>(holidayDb);

        return holiday;
    }
}