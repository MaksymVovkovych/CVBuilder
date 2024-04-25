using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.EFContext.Configurations;

public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    public void Configure(EntityTypeBuilder<Proposal> builder)
    {
        builder.ToTable("Proposal");
        builder.HasMany(x => x.Resumes).WithOne(x => x.Proposal);
        builder.HasOne(x => x.CreatedUser).WithMany("CreatedProposals").OnDelete(DeleteBehavior.ClientSetNull);
        builder.HasOne(x => x.Client).WithMany("ClientProposals").OnDelete(DeleteBehavior.ClientSetNull);
    }
}