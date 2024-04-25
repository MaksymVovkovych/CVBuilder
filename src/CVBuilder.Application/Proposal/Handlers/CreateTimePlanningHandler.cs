using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

public class CreateTimePlanningHandler : IRequestHandler<CreateTimePlanningCommand, List<WorkDayResult>>
{
    private readonly IRepository<WorkDay, int> _workDayRepository;
    private readonly IRepository<Holiday, int> _holidayRepository;
    private readonly IMapper _mapper;

    public CreateTimePlanningHandler(IRepository<WorkDay, int> workDayRepository,
        IRepository<Holiday, int> holidayRepository, IMapper mapper)
    {
        _workDayRepository = workDayRepository;
        _holidayRepository = holidayRepository;
        _mapper = mapper;
    }

    public async Task<List<WorkDayResult>> Handle(CreateTimePlanningCommand request,
        CancellationToken cancellationToken)
    {
        var date = request.Date.Date;
        var isWeekMode = request.IsWeekMode.GetValueOrDefault();

        int countDays;

        var days = new List<DateTime>();
        
        DateTime startDate;


        if (isWeekMode)
        {
            countDays = 7;
            startDate = date.AddDays(-(int)date.DayOfWeek);
            for (var i = 0; i < countDays; i++)
            {
                days.Add(startDate.AddDays(i));
            }  
        }
        else
        {
            countDays = DateTime.DaysInMonth(date.Year, date.Month);
            startDate = date.AddDays(-(date.Day-1));
            for (var i = 0; i < countDays; i++)
            {
                days.Add(startDate.AddDays(i));
            }

        }
        var endDate = startDate.AddDays(countDays-1);

        var workDays = await _workDayRepository.Table
            .Where(x => x.ProposalResumeId == request.ProposalResumeId &&
                        x.Date.Date >= startDate.Date &&
                        x.Date.Date <= endDate.Date)
            .OrderBy(x => x.Date)
            .ToListAsync(cancellationToken: cancellationToken);


        var holidays = await _holidayRepository.Table.ToListAsync(cancellationToken: cancellationToken);


        foreach (var day in days!)
        {
            var workDay = workDays.FirstOrDefault(x => x.Date.Date == day.Date);

            var isNull = false;

            if(workDay == null)
            {
                isNull = true;
                workDay = new WorkDay
                {
                    ProposalResumeId = request.ProposalResumeId,
                    CountHours = request.CountHours,
                    Date = day.Date,
                };
            }
            else 
            {
                workDay.UpdatedAt = DateTime.UtcNow;
                workDay.CountHours = request.CountHours;
            }

            if (request.ExceptWeekends && day.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                workDay.CountHours = 0;
            }

            if (request.ExceptHolidays && holidays.Any(x => x.Date.Date == workDay.Date.Date))
            {
                workDay.CountHours = 0;
            }

            if (isNull)
                workDays.Add(workDay);
        }

        await _workDayRepository.UpdateRangeAsync(workDays);


        workDays = workDays.OrderBy(x => x.Date).ToList();

        var result = _mapper.Map<List<WorkDayResult>>(workDays);

        return result;
    }
}