using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBuilder.Application.Core.Settings;
using CVBuilder.EFContext;
using CVBuilder.EFContext.Extensions;
using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CVBuilder.Web.Infrastructure.Extensions;

public static class EncryptedMigratorExtension
{
    public static IServiceCollection AddEfCoreEncrypt(this IServiceCollection services, IConfiguration configuration)
    {
        var encryptSettings = configuration.GetSection(nameof(EncryptSettings));

        var key = encryptSettings[nameof(EncryptSettings.Key)]!;
        var iv = encryptSettings[nameof(EncryptSettings.Iv)]!;
        var byteKey = Convert.FromBase64String(key);
        var ivKey = Convert.FromBase64String(iv);

        var encryptor = new AesProvider(byteKey, ivKey);

        
        
        
        var migrationProvider = new MigrationProvider(encryptor);

        
        services.AddSingleton<IEncryptionProvider>(migrationProvider);
        services.AddSingleton<IEncryptionProvider>(encryptor);

        return services;
    }

    public static async Task<IHost> EncryptData(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        await using var context = services.GetService<IdentityEfDbContext>();

        await MigrateResume(context);
        await MigrateUser(context);
        await MigrateResumeHistory(context);
        await MigrateExpense(context);
        await MigrateExperience(context);
        await MigrateProposal(context);
        
        await context!.SaveChangesAsync();
        return host;
    }

    private static async Task MigrateProposal(DbContext context)
    {
        var properties = new List<string>
        {
            nameof(Proposal.ProposalName)
        };

        await Migrator<Proposal>(context, properties);
    }

    private static async Task MigrateExperience(DbContext context)
    {
        var properties = new List<string>
        {
            nameof(Experience.Company), nameof(Experience.Position),
            nameof(Experience.Description),
        };

        await Migrator<Experience>(context, properties);
    }


    private static async Task MigrateResume(DbContext context)
    {
        var properties = new List<string>
        {
            nameof(Resume.FirstName), nameof(Resume.LastName), nameof(Resume.ResumeName),
            nameof(Resume.Picture), nameof(Resume.Email), nameof(Resume.Site),
            nameof(Resume.Phone), nameof(Resume.Code), nameof(Resume.Country),
            nameof(Resume.City), nameof(Resume.Street), nameof(Resume.RequiredPosition),
            nameof(Resume.Birthdate), nameof(Resume.AboutMe), nameof(Resume.SalaryRate),
            nameof(Resume.Hobbies),
        };

        await Migrator<Resume>(context, properties);
    }
    private static async Task MigrateUser(DbContext context)
    {
        var properties = new List<string>
        {
            nameof(User.IdentityUser.FirstName), nameof(User.IdentityUser.LastName), nameof(User.Site),
            nameof(User.Contacts), nameof(User.CompanyName), nameof(User.Site),
            nameof(User.NormalizedEmail), nameof(User.NormalizedUserName), nameof(User.IdentityUser.UserName), nameof(User.IdentityUser.Email),
        };

        await Migrator<User>(context,  properties);
    }
    private static async Task MigrateResumeHistory(DbContext context)
    {
        var properties = new List<string>
        {
            nameof(ResumeHistory.OldResumeJson), 
            nameof(ResumeHistory.NewResumeJson)
        };

        await Migrator<ResumeHistory>(context, properties);
    }
    private static async Task MigrateExpense(DbContext context)
    {
        var properties = new List<string>
        {
            nameof(Expense.ExpenseName), 
            nameof(Expense.Amount), 
            nameof(Expense.SummaryDollars), 
        };

        await Migrator<Expense>(context, properties);
    }
    private static async Task Migrator<TEntity>(DbContext context, List<string> properties) where TEntity : class
    {
        var objects = await context.Set<TEntity>().ToListAsync();
        foreach (var obj in objects)
        {
            var entry = context.Entry(obj);
            foreach (var property in properties)
            {
                entry.Property(property).IsModified = true;
            }
        }
    }
   
}