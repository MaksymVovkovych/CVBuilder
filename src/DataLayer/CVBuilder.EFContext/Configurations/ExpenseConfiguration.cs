using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.EFContext.Configurations;

public class ExpenseConfiguration: IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.Property(x => x.Amount)
            // .HasPrecision(18,4)
            ;
        builder.Property(x => x.SummaryDollars)
            // .HasPrecision(18,4)
            ;

    }
}