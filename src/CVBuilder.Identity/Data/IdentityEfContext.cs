
using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Identity.Data;

public class IdentityEfContext : DbContext
{
    public IdentityEfContext(DbContextOptions<IdentityEfContext> options) : base(options)
    {
        
    }

    public DbSet<IdentityUser> IdentityUsers { get; set; }
}