using CVBuilder.Models.Entities;
using MigrationTool.DbContexts;

namespace MigrationTool;

public class DecryptData
{
    private EncryptDbContext encryptDbContext;
    private DecryptDbContext dencryptDbContext;
    
    
    public DecryptData(EncryptDbContext encryptDbContext, DecryptDbContext dencryptDbContext)
    {
        this.encryptDbContext = encryptDbContext;
        this.dencryptDbContext = dencryptDbContext;
    }

    private IEnumerable<User> EncryptedUsers => encryptDbContext.Users.ToList();
    private IEnumerable<Resume> EncryptedResumes => encryptDbContext.Resumes.ToList();
    private IEnumerable<Expense> EncryptedExpenses => encryptDbContext.Expenses.ToList();
    private IEnumerable<ResumeHistory> EncryptedrResumeHistories => encryptDbContext.ResumeHistories.ToList();
    private IEnumerable<Experience> EncryptedExperiences => encryptDbContext.Experiences.ToList();
    private IEnumerable<Proposal> EncryptedProposal => encryptDbContext.Proposals.ToList();

    public void Decrypt()
    {
        
        
    }
    
}