using System;
using System.Globalization;
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

public class UpdateExpenseHandler: IRequestHandler<UpdateExpenseCommand, ExpenseResult>
{
    private readonly IRepository<Expense,int> _expenseRepository;
    private readonly IMapper _mapper;
    private readonly ICurrencyService _currencyService;
    
    public UpdateExpenseHandler(IRepository<Expense, int> expenseRepository, IMapper mapper, ICurrencyService currencyService)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
        _currencyService = currencyService;
    }

    public async Task<ExpenseResult> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.Table
            .FirstOrDefaultAsync(x => x.Id == request.ExpenseId, cancellationToken: cancellationToken);
        
        if(expense == null)
            throw new NotFoundException("Expense not found");

        await UpdateExpenseFromRequest(expense, request);

        expense = await _expenseRepository.UpdateAsync(expense);

        var result = _mapper.Map<ExpenseResult>(expense);

        return result;
    }

    private async Task UpdateExpenseFromRequest(Expense expense, UpdateExpenseCommand request)
    {
        var summaryDollar = request.Currency switch
        {
            Currency.Usd => request.Amount,
            Currency.Hryvnia => await GetCurrencyUahToUsd(request),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        expense.AmountDecimal = request.Amount;
        expense.ExpenseName = request.ExpenseName;
        expense.Currency = request.Currency;
        expense.ExpenseType = request.ExpenseType;
        expense.SummaryDollarsDecimal = summaryDollar;
        expense.UpdatedAt = DateTime.UtcNow;
    }

    private async Task<decimal> GetCurrencyUahToUsd(UpdateExpenseCommand request)
    {
        var currency = await _currencyService.GetCurrencyAsync(CurrencyConstants.USD);
        var currencyDollarToHryvnia = decimal.Parse(currency.Sale, NumberStyles.Currency, CultureInfo.InvariantCulture);
        var dollars = request.Amount / currencyDollarToHryvnia;
        return Math.Round(dollars, 3);
    }
}