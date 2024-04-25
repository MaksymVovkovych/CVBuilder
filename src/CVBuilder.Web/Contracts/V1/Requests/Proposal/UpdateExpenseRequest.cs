
using CVBuilder.Models;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class UpdateExpenseRequest
{
    public int ExpenseId { get; set; }
    public string ExpenseName { get; set; }
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
    public ExpenseType ExpenseType { get; set; }
}