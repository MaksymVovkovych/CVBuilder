using System;
using CVBuilder.Models;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class CreateExpenseRequest
{
    public DateTime Month { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public string ExpenseName { get; set; }
}