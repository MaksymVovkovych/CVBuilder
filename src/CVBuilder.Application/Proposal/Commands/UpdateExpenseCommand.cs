using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using MediatR;

namespace CVBuilder.Application.Proposal.Commands;

public class UpdateExpenseCommand : IRequest<ExpenseResult>
{
    public int ExpenseId { get; set; }
    public string ExpenseName { get; set; }
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
    public ExpenseType ExpenseType { get; set; }
}