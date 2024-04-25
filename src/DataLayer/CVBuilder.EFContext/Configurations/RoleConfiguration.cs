using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.EFContext.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasMany(p => p.Users)
            .WithMany(p => p.Roles)
            .UsingEntity<UserRole>(
                j => j
                    .HasOne(pt => pt.User)
                    .WithMany(t => t.UserRoles)
                    .HasForeignKey(pt => pt.UserId),
                j => j
                    .HasOne(pt => pt.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(pt => pt.RoleId),
                j => { j.HasKey(t => new { t.UserId, t.RoleId }); });
    }
}