using System;
using System.Collections.Generic;
using CVBuilder.Models;

namespace CVBuilder.Web.Contracts.V1.Requests.Proposal;

public class DuplicateExpensesRequest
{
    public DateTime FromMonth { get; set; }
    public DateTime ToMonth { get; set; }
    public List<ExpenseType> ExpenseTypes { get; set; }
}