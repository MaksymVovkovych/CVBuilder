using CVBuilder.EFContext.Configurations;
using CVBuilder.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using File = CVBuilder.Models.Entities.File;

namespace MigrationToolDecrypt.DbContexts;

public class EncryptDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    
    private readonly IEncryptionProvider _encryptionProvider;
    
    public EncryptDbContext(DbContextOptions<EncryptDbContext> options, IEncryptionProvider provider, IEncryptionProvider encryptionProvider)
        : base(options)
    {
        _encryptionProvider = encryptionProvider;
    }

    #region D

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    public virtual DbSet<Holiday> Holidays { get; set; }
    public virtual DbSet<Resume> Resumes { get; set; }
    public virtual DbSet<Education> Educations { get; set; }
    public virtual DbSet<Experience> Experiences { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<Language> Languages { get; set; }
    public virtual DbSet<ProposalBuild> ProposalBuilds { get; set; }
    public virtual DbSet<Skill> Skills { get; set; }
    public virtual DbSet<File> Files { get; set; }
    public virtual DbSet<ResumeHistory> ResumeHistories { get; set; }
    public virtual DbSet<Proposal> Proposals { get; set; }
    public virtual DbSet<ProposalResume> ProposalResumes { get; set; }
    public virtual DbSet<LevelSkill> LevelSkills { get; set; }
    public virtual DbSet<LevelLanguage> LevelLanguages { get; set; }
    public virtual DbSet<Expense> Expenses { get; set; }
    public virtual DbSet<User> AspNetUsers { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }

    #endregion


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProposalConfiguration).Assembly);
        modelBuilder.UseEncryption(_encryptionProvider);
    }
}