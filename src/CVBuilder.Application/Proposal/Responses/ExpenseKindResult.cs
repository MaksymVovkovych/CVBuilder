using System.Collections.Generic;
using CVBuilder.Models;

namespace CVBuilder.Application.Proposal.Responses;


public class ExpenseKindResult
{
    public ExpenseType ExpenseType { get; set; }
    public decimal SummaryDollars  { get; set; }
    public List<ExpenseResult> Expenses { get; set; }
}

public class ExpenseResult
{
    public int ExpenseId { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public string ExpenseName { get; set; }
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
    public decimal SummaryDollars { get; set; }
}