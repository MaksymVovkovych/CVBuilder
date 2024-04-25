using CVBuilder.EFContext.Configurations;
using CVBuilder.Models.Entities;
using CVBuilder.Models.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;

namespace MigrationToolDecrypt.DbContexts;

public class DecryptDbContext : IdentityDbContext<, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DecryptDbContext(DbContextOptions<DecryptDbContext> options)
        : base(options)
   {
          
   }
    
    public virtual DbSet<Experience> Experiences { get; set; }
    public virtual DbSet<Proposal> Proposals { get; set; }
    public virtual DbSet<User> AspNetUsers { get; set; }
    public virtual DbSet<Expense> Expenses { get; set; }
    public virtual DbSet<ResumeHistory> ResumeHistories { get; set; }
    public virtual DbSet<Resume> Resumes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProposalConfiguration).Assembly);
    }
}