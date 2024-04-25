using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.EFContext.Configurations;

public class ComplexityConfiguration : IEntityTypeConfiguration<ProposalBuild>
{
    public void Configure(EntityTypeBuilder<ProposalBuild> builder)
    {
        builder.HasOne(x => x.Complexity).WithMany(x => x.ProposalBuilds).OnDelete(DeleteBehavior.SetNull);
    }
}