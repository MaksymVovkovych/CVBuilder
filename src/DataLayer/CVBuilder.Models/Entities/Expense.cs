using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Expense: Entity<int>
{
    public DateTime Month { get; set; }
    public ExpenseType ExpenseType { get; set; }
    [Encrypted]
    public string ExpenseName { get; set; }
    public Currency Currency { get; set; }
    
    [Encrypted]
    public string Amount { get; set; }
    [NotMapped]
    public decimal AmountDecimal
    {
        get => CustomConverter.ToDecimal(Amount)!.Value;
        set => Amount = value.ToString(CultureInfo.InvariantCulture);
    }
    
    [Encrypted]
    public string SummaryDollars { get; set; }
    
    [NotMapped]
    public decimal SummaryDollarsDecimal
    {
        get => CustomConverter.ToDecimal(SummaryDollars)!.Value;
        set => SummaryDollars = value.ToString(CultureInfo.InvariantCulture);
    }
}