using System;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using MediatR;

namespace CVBuilder.Application.Proposal.Commands;

public class CreateExpenseCommand: IRequest<ExpenseResult>
{
    public DateTime Month { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public string ExpenseName { get; set; }
}