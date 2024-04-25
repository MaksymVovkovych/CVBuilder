using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;

namespace MigrationTool.DbContexts;

public class DecryptDbContext : DbContext
{
    private readonly IEncryptionProvider _encryptionProvider;
    public DecryptDbContext(DbContextOptions<EncryptDbContext> options, IEncryptionProvider provider, IEncryptionProvider encryptionProvider)
        : base(options)
    {
        _encryptionProvider = encryptionProvider;
    }
    
    public virtual DbSet<Experience> Experiences { get; set; }
    public virtual DbSet<Proposal> Proposals { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Expense> Expenses { get; set; }
    public virtual DbSet<ResumeHistory> ResumeHistories { get; set; }
    public virtual DbSet<Resume> Resumes { get; set; }
}