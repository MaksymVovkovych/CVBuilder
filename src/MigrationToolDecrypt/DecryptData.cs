using MigrationToolDecrypt.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MigrationToolDecrypt;


public  class DecryptData
{
    private readonly DecryptDbContext _decryptDbContext;
    private readonly EncryptDbContext _encryptDbDbContext;

    public DecryptData(DecryptDbContext decryptDbContext, EncryptDbContext encryptDbDbContext)
    {
        _decryptDbContext = decryptDbContext;
        _encryptDbDbContext = encryptDbDbContext;
    }

    public async Task DecryptEncryptedData()
    {
        
        var users = await _encryptDbDbContext.Users.AsNoTracking().ToArrayAsync();
        var expenses = await _encryptDbDbContext.Expenses.AsNoTracking().ToArrayAsync();
        var resumes = await _encryptDbDbContext.Resumes.AsNoTracking().ToArrayAsync();
        var resumeHistories = await _encryptDbDbContext.ResumeHistories.AsNoTracking().ToArrayAsync();
        var experience = await _encryptDbDbContext.Experiences.AsNoTracking().ToArrayAsync();
        var proposals = await _encryptDbDbContext.Proposals.AsNoTracking().ToArrayAsync();
        
        _decryptDbContext.Expenses.UpdateRange(expenses);
        _decryptDbContext.Users.UpdateRange(users);
        _decryptDbContext.Resumes.UpdateRange(resumes);
        _decryptDbContext.ResumeHistories.UpdateRange(resumeHistories);
        _decryptDbContext.Proposals.UpdateRange(proposals);
        _decryptDbContext.Experiences.UpdateRange(experience);

        await _decryptDbContext.SaveChangesAsync();

        

    }
}