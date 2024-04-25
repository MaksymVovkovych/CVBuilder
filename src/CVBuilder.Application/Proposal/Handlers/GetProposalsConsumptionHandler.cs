using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using CVBuilder.Repository;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

using Models.Entities;

public class GetProposalsConsumptionHandler : IRequestHandler<GetProposalsConsumptionQuery, ConsumptionResult>
{
    private readonly IRepository<Proposal, int> _proposalRepository;

    public GetProposalsConsumptionHandler(IRepository<Proposal, int> proposalRepository)
    {
        _proposalRepository = proposalRepository;
    }

    public async Task<ConsumptionResult> Handle(GetProposalsConsumptionQuery request,
        CancellationToken cancellationToken)
    {
        if (request.FromDate < request.ToDate)
            throw new ValidationException("Wrong date range");

        var fromDate = request.FromDate?.Date ?? DateTime.UtcNow.Date;
        fromDate = fromDate.AddDays(-(fromDate.Day - 1));
        var toDate = request.ToDate?.Date ?? fromDate.AddYears(-1).AddMonths(1);
        toDate = toDate.AddDays(-(toDate.Day - 1));
        toDate = toDate.AddMonths(-1);
        
        var countMonth = (fromDate.Year - toDate.Year) * 12 + fromDate.Month - toDate.Month;
        
        var query = _proposalRepository.TableNoTracking
            .Include(x => x.Resumes)
            .ThenInclude(x => x.Resume)
            .ThenInclude(x => x.Position)
            .Include(x => x.Resumes)
            .ThenInclude(x => x.WorkDays)
            .Where(x => x.StatusProposal == StatusProposal.InWorking);

        if (request.ProposalId.HasValue)
            query = query.Where(x => x.Id == request.ProposalId.Value);

        var proposals = await query.ToListAsync(cancellationToken: cancellationToken);
       
        var proposalConsumptions = MapConsumptions(proposals,fromDate,countMonth);

        var months = new List<DateTime>(countMonth);
        for (var i = 0; i < countMonth; i++)
            months.Add(fromDate.AddMonths(-i));

        return new ConsumptionResult
        {
            Months = months,
            Proposals = proposalConsumptions.ToList()
        };
    }

    private static IEnumerable<ProposalConsumptionResult> MapConsumptions(List<Proposal> proposals, DateTime fromDate, int countMonth)
    {
        foreach (var proposal in proposals)
        {
            var consumptionResumes = new List<ConsumptionResumeResult>(proposal.Resumes.Count);
            foreach (var proposalResume in proposal.Resumes)
            {
                var summaryOfMonths = new List<decimal>(countMonth);
                for (var i = 0; i < countMonth; i++)
                {
                    var firstDayCurrentMonth = fromDate.AddMonths(-i);
                    var firstDayNextMonth = firstDayCurrentMonth.AddMonths(1);
                    
                    var workDays = proposalResume.WorkDays
                        .Where(x => firstDayCurrentMonth <= x.Date && x.Date < firstDayNextMonth);
                    
                    var summaryOfMonth = workDays
                         .Sum(x => x.CountHours * proposalResume.Resume.SalaryRateDecimal.GetValueOrDefault());
                    
                    summaryOfMonths.Add(summaryOfMonth);
                }

                consumptionResumes.Add(new ConsumptionResumeResult
                {
                    ProposalResumeId = proposalResume.Id,
                    FirstName = proposalResume.Resume.FirstName,
                    LastName = proposalResume.Resume.LastName,
                    Position = new PositionResult
                    {
                        PositionId = proposalResume.Resume.Position.Id,
                        PositionName = proposalResume.Resume.Position.PositionName
                    },
                    SummaryOfMonths = summaryOfMonths,
                    Summary = summaryOfMonths.Sum()
                });
            }

            var summaryByMonth = new List<decimal>(countMonth);
            for (var i = 0; i < countMonth; i++)
            {
                var summaryOneMonth = consumptionResumes.Select(resume => resume.SummaryOfMonths[i]).Sum();
                summaryByMonth.Add(summaryOneMonth);
            }
            

            yield return new ProposalConsumptionResult
            {
                ProposalId = proposal.Id,
                ProposalName = proposal.ProposalName,
                Summary = consumptionResumes.Sum(x => x.Summary),
                Resumes = consumptionResumes,
                SummaryByMonths = summaryByMonth
            };
        }
    }
}