using System;
using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Models;
using MediatR;

namespace CVBuilder.Application.Proposal.Commands;

public class DuplicateExpensesCommand: IRequest<List<ExpenseKindResult>>
{
    public DateTime FromMonth { get; set; }
    public DateTime ToMonth { get; set; }
    public List<ExpenseType> ExpenseTypes { get; set; }
}