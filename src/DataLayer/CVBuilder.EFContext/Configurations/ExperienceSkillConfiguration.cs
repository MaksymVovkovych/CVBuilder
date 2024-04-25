using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.EFContext.Configurations;

public class ExperienceSkillConfiguration : IEntityTypeConfiguration<ExperienceSkill>
{
    public void Configure(EntityTypeBuilder<ExperienceSkill> builder)
    {
        builder.ToTable("ExperiencesSkills");

        builder.HasOne(x => x.Experience)
            .WithMany(x => x.Skills)
            .HasForeignKey(x => x.ExperienceId);

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Experiences)
            .HasForeignKey(x => x.SkillId);
    }
}