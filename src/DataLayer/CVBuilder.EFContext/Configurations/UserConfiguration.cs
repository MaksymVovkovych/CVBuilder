using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVBuilder.EFContext.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.NormalizedEmail).IsEncrypted();
        builder.Property(x => x.NormalizedUserName).IsEncrypted();
        builder.Property(x => x.IdentityUser.FullName).IsEncrypted();
        builder.Property(x => x.IdentityUser.Email).IsEncrypted();
    }
}