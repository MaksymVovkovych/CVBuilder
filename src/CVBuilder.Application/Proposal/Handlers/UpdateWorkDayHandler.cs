using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;

public class
    UpdateWorkDayHandler : IRequestHandler<UpdateWorkDayCommand, WorkDayResult>
{
    private readonly IRepository<WorkDay, int> _workDaysRepository;
    private readonly IMapper _mapper;


    public UpdateWorkDayHandler(IRepository<WorkDay, int> workDaysRepository,
        IMapper mapper)
    {
        _workDaysRepository = workDaysRepository;
        _mapper = mapper;
    }


    public async Task<WorkDayResult> Handle(UpdateWorkDayCommand request,
        CancellationToken cancellationToken)
    {
        var requestWorkDay = request.WorkDay;

        var workDay = await _workDaysRepository.Table
            .FirstOrDefaultAsync(x => x.ProposalResumeId == request.ProposalResumeId
                                      && (x.Date.Date == requestWorkDay.Date.Date || x.Id == requestWorkDay.Id),
                cancellationToken: cancellationToken);
      
        if (workDay != null)
        {
            workDay.CountHours = requestWorkDay.CountHours;
            workDay.UpdatedAt = DateTime.UtcNow;
        }
        else
            workDay = new WorkDay
            {
                Date = requestWorkDay.Date,
                ProposalResumeId = request.ProposalResumeId,
                CountHours = requestWorkDay.CountHours
            };


        workDay =  await _workDaysRepository.UpdateAsync(workDay);

        var result = _mapper.Map<WorkDayResult>(workDay);

        return result;
    }
}