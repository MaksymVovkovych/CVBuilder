using MediatR;

namespace CVBuilder.Application.Proposal.Commands;

public class DeleteExpenseCommand: IRequest<bool>
{
    public int ExpenseId { get; set; }
}