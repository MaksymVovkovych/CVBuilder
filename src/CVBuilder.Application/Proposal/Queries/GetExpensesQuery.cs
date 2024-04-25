using System;
using System.Collections.Generic;
using CVBuilder.Application.Proposal.Responses;
using MediatR;

namespace CVBuilder.Application.Proposal.Queries;

public class GetExpensesQuery: IRequest<List<ExpenseKindResult>>
{
    public DateTime? Month { get; set; }
}