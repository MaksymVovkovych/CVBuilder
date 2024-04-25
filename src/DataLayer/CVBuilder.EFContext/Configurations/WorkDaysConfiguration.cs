using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.EFContext.Configurations;

public class WorkDaysConfiguration: IEntityTypeConfiguration<WorkDay>
{
    public void Configure(EntityTypeBuilder<WorkDay> builder)
    {
        builder.HasOne(x => x.ProposalResume).WithMany(x => x.WorkDays);
    }
}