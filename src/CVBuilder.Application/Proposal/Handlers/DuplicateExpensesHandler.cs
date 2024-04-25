using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Queries;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

public class DuplicateExpensesHandler : IRequestHandler<DuplicateExpensesCommand, List<ExpenseKindResult>>
{
    private readonly IRepository<Expense, int> _expenseRepository;
    private readonly IMediator _mediator;
    
    public DuplicateExpensesHandler(IRepository<Expense, int> expenseRepository, IMediator mediator)
    {
        _expenseRepository = expenseRepository;
        _mediator = mediator;
    }

    public async Task<List<ExpenseKindResult>> Handle(DuplicateExpensesCommand request, CancellationToken cancellationToken)
    {
        var fromMonth = request.FromMonth.Date.AddDays(-(request.FromMonth.Day - 1));
        var toMonth = request.ToMonth.Date.AddDays(-(request.ToMonth.Day - 1));

        var expensesFromMonth = await _expenseRepository.TableNoTracking.Where(x => x.Month == fromMonth)
            .ToListAsync(cancellationToken: cancellationToken);

        var expensesToMonth = await _expenseRepository.Table.Where(x => x.Month == toMonth)
            .ToListAsync(cancellationToken: cancellationToken);

        DuplicateExpenses(expensesFromMonth, expensesToMonth,request.ExpenseTypes,toMonth);

         await _expenseRepository.UpdateRangeAsync(expensesToMonth);
        
        var results = await _mediator.Send(new GetExpensesQuery
        {
            Month = toMonth,
        }, cancellationToken);

        return results;
    }

    private static void DuplicateExpenses(List<Expense> expensesFromMonth, List<Expense> expensesToMonth, List<ExpenseType> expenseTypes, DateTime toMonth)
    {
        foreach (var expenseType in expenseTypes)
        {
            var expensesByType = expensesFromMonth.Where(x => x.ExpenseType == expenseType);
            foreach (var expenseFromMonth in expensesByType)
            {
                var isExpenseExists = expensesToMonth
                    .Any(x => x.ExpenseName == expenseFromMonth.ExpenseName &&
                              x.ExpenseType == expenseFromMonth.ExpenseType);

                if (!isExpenseExists)
                {
                    expenseFromMonth.Id = 0;
                    expenseFromMonth.Month = toMonth;
                    expensesToMonth.Add(expenseFromMonth);
                }
            }
        }
    }
    
}