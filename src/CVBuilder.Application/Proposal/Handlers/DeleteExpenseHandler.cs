using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Proposal.Commands;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.Proposal.Handlers;

public class DeleteExpenseHandler: IRequestHandler<DeleteExpenseCommand,bool>
{
    private readonly IRepository<Expense,int> _expenseRepository;

    public DeleteExpenseHandler(IRepository<Expense, int> expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<bool> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        await _expenseRepository.SoftDeleteAsync(request.ExpenseId);
        return true;
    }
}