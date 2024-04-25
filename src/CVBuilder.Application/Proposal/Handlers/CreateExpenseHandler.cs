using System;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Core.Constants;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Application.Proposal.Services.Interfaces;
using CVBuilder.Models;
using CVBuilder.Models.Entities;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.Proposal.Handlers;

public class CreateExpenseHandler : IRequestHandler<CreateExpenseCommand, ExpenseResult>
{
    private readonly IRepository<Expense, int> _expenseRepository;
    private readonly IMapper _mapper;
    private readonly ICurrencyService _currencyService;
    public CreateExpenseHandler(IRepository<Expense, int> expenseRepository, IMapper mapper, ICurrencyService currencyService)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
        _currencyService = currencyService;
    }

    public async Task<ExpenseResult> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var month = request.Month.Date.AddDays(-(request.Month.Day - 1));
        request.ExpenseName = request.ExpenseName.Trim();

        var isExpenseExists = await _expenseRepository.TableNoTracking.AnyAsync(x =>
                x.ExpenseType == request.ExpenseType &&
                x.ExpenseName == request.ExpenseName &&
                x.Month == month,
            cancellationToken: cancellationToken);

        if (isExpenseExists)
            throw new ConflictException("Expense already exists");

   
        
        var summaryDollars = request.Currency switch
        {
            Currency.Usd => request.Amount,
            Currency.Hryvnia => await GetCurrencyUahToUsd(request),
            _ => throw new ArgumentOutOfRangeException()
        };

        
        var expense = _mapper.Map<Expense>(request);
        expense.SummaryDollarsDecimal = summaryDollars;
        expense.Month = month;
        expense = await _expenseRepository.CreateAsync(expense);

        var result = _mapper.Map<ExpenseResult>(expense);

        return result;
    }
    
    private async Task<decimal> GetCurrencyUahToUsd(CreateExpenseCommand request)
    {
        var currency = await _currencyService.GetCurrencyAsync(CurrencyConstants.USD);
        var currencyDollarToHryvnia = decimal.Parse(currency.Sale, NumberStyles.Currency, CultureInfo.InvariantCulture);
        var dollars = request.Amount / currencyDollarToHryvnia;
        dollars = Math.Round(dollars, 3);
        return dollars;
    }
}