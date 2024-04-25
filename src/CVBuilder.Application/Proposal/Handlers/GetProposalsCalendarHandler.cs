using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;

public class GetProposalsCalendarHandler : IRequestHandler<GetProposalsCalendarQuery, CalendarResult>
{
    private readonly IRepository<Proposal, int> _proposalRepository;
    private readonly IMapper _mapper;

    public GetProposalsCalendarHandler(IRepository<Proposal, int> proposalRepository, IMapper mapper)
    {
        _proposalRepository = proposalRepository;
        _mapper = mapper;
    }

    public async Task<CalendarResult> Handle(GetProposalsCalendarQuery request,
        CancellationToken cancellationToken)
    {
        var proposals = await _proposalRepository.TableNoTracking
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .ThenInclude(x => x.Position)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.WorkDays)
            .Where(x=>x.StatusProposal == StatusProposal.InWorking)
            .ToListAsync(
                cancellationToken: cancellationToken);


        request.Date ??= DateTime.UtcNow;
        var date = request.Date.Value.Date;

        var isWeekMode = request.IsWeekMode.GetValueOrDefault();

        int countDays;
        
        DateTime startDate;


        if (isWeekMode)
        {
            countDays = 7;
            startDate = date.AddDays(-(int)date.DayOfWeek);
           
        }
        else
        {
            countDays = DateTime.DaysInMonth(date.Year, date.Month);
            startDate = date.AddDays(-(date.Day - 1));

        }
        var endDate = startDate.AddDays(countDays-1);
        

        FilterWorkDaysByDate(proposals, startDate,endDate);

        var days = GenerateDates(date, request.IsWeekMode.GetValueOrDefault());
        
        GenerateWorkDays(proposals, days);

        var result = _mapper.Map<List<ProposalCalendarResult>>(proposals);
            
        return new CalendarResult
        {
            Proposals = result,
            Days = days,
        };
    }

    private static void FilterWorkDaysByDate(IEnumerable<Proposal> proposals, DateTime startDate, DateTime endDate)
    {
        foreach (var proposalResume in proposals.SelectMany(proposal => proposal.Resumes))
        {
            proposalResume.WorkDays = proposalResume.WorkDays
                .Where(x => x.Date.Date >= startDate && x.Date.Date <= endDate)
                .OrderBy(x => x.Date).ToList();
        }
    }

    private static List<DateTime> GenerateDates(DateTime date, bool isWeekMode = true)
    {
        var start = date.AddDays(-(int)date.DayOfWeek);
        var days = new List<DateTime>();

        int countDays;
        
        if (isWeekMode)
        {
            countDays = 7;
            for (var i = 0; i < countDays; i++)
            {
                days.Add(start.AddDays(i));
            }  
        }
        else
        {
            countDays = DateTime.DaysInMonth(date.Year, date.Month);
            start = date.AddDays(-(date.Day-1));
            for (var i = 0; i < countDays; i++)
            {
                days.Add(start.AddDays(i));
            }   
        }

        return days;
    }

    private static void GenerateWorkDays(IEnumerable<Proposal> proposals, List<DateTime> days)
    {
        
        foreach (var proposalResume in proposals.SelectMany(x => x.Resumes))
        {
            var workDays = proposalResume.WorkDays;

            foreach (var day in days)
            {
                var workDay = workDays.FirstOrDefault(x => x.Date == day.Date);
                if (null == workDay)
                {
                    workDays.Add(new WorkDay
                    {
                        Date = day.Date
                    });
                }
                else
                    workDay.Date = DateTime.SpecifyKind(workDay.Date, DateTimeKind.Utc);
            }

            proposalResume.WorkDays = workDays.OrderBy(x => x.Date).ToList();
        }
    }
}