using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CVBuilder.Application.Proposal.Handlers;

public class GetExpensesHandler : IRequestHandler<GetExpensesQuery, List<ExpenseKindResult>>
{
    private readonly IRepository<Expense, int> _expenseRepository;
    private readonly IMapper _mapper;
    
    public GetExpensesHandler(IRepository<Expense, int> expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<List<ExpenseKindResult>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        var month = request.Month?.Date ?? DateTime.UtcNow.Date;
        month = month.AddDays(-(month.Day - 1));

        var expenses = await _expenseRepository.Table
            .Where(x => x.Month == month)
            .ToListAsync(cancellationToken);
        
        var results = MapObjects(expenses);


        return results;
    }

    private List<ExpenseKindResult> MapObjects(IReadOnlyCollection<Expense> expenses)
    {
        var expensiveTypes = Enum.GetValues<ExpenseType>().ToList();
        var results = new List<ExpenseKindResult>(expensiveTypes.Count);
        
        foreach (var expensiveType in expensiveTypes)
        {
            var expensesByType = expenses.Where(x => x.ExpenseType == expensiveType).ToList();
            var expensesResult = _mapper.Map<List<ExpenseResult>>(expensesByType);        
            
            results.Add(new ExpenseKindResult
            {
                ExpenseType = expensiveType,
                Expenses = expensesResult,
                SummaryDollars = expensesResult.Sum(x=>x.SummaryDollars)
            });
        }

        return results;
    }
}