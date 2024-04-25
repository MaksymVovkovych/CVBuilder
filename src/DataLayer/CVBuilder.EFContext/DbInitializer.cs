using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVBuilder.EFContext.Extensions;
using CVBuilder.Models;
using CVBuilder.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.EFContext;

public class DbInitializer
{
    public static async Task Initialize(IdentityEfDbContext context)
    {
        if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();

        await SeedLanguageAsync(context);
        await SeedSkillAsync(context);
        await SeedCv(context);


        var enums = new[]
        {
            "caacb290-2ddc-41ee-a7fe-bc4486d864d5",
            "2c00ec4e-9928-425a-9fd3-9654e6dd98c1",
            "9b9e21a8-faa9-4059-bea1-46bbc706857b",
            "640edb28-9db8-42f7-b88a-f22c593b00d1",
            "17636b07-4f31-4963-9347-73abf96b851d",
            "bd633b3b-4c42-4268-9a6e-9063ed7c9dcd",
            "a3036098-6c1e-4ec6-99fc-9130f1f055fe"
        };

        var index = 0;
        var types = EnumExtensions.EnumToList<RoleTypes>();
        var normalizer = new UpperInvariantLookupNormalizer();

        foreach (var (key, _) in types)
        {
            if (context.Roles.FirstOrDefault(e => e.Name == key.ToString()) == null)
                context.Roles.Add(
                    new Role
                    {
                        Name = key.ToString(),
                        NormalizedName = normalizer.NormalizeName(key.ToString()),
                        ConcurrencyStamp = enums[index],
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                );

            index++;
        }

        await context.SaveChangesAsync();
        await context.Database.EnsureCreatedAsync();
    }

    private static async Task SeedLanguageAsync(IdentityEfDbContext context)
    {
        if (!await context.Languages.AnyAsync())
        {
            await context.Languages.AddRangeAsync(new List<Language>
            {
                new()
                {
                    Name = "english"
                },
                new()
                {
                    Name = "russian"
                },
                new()
                {
                    Name = "ukrainian"
                }
            });

            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedSkillAsync(IdentityEfDbContext context)
    {
        if (!await context.Skills.AnyAsync())
        {
            await context.Skills.AddRangeAsync(new List<Skill>
            {
                new()
                {
                    Name = "c#"
                },
                new()
                {
                    Name = "jq"
                },
                new()
                {
                    Name = "oop"
                }
            });

            await context.SaveChangesAsync();
        }
    }


    private static async Task SeedCv(IdentityEfDbContext context)
    {
        if (await context.Resumes.AnyAsync()) return;

        var skills = await context.Skills.Take(2).ToListAsync();

        var languages = await context.Languages.Take(2).ToListAsync();

        var cv = new Resume
        {
            FirstName = "Andrii",
            LastName = "Shevchuk",
            AboutMe = "I an is a .net developer",
            City = "Kiev",
            Birthdate = "12/05/1999",
            Code = "00010",
            Country = "Ukraine",
            Phone = "+380987211728",
            CreatedAt = DateTime.Now,
            Email = "andrii99.shevchuk@gmail.com",
            Site = "todo",
            DeletedAt = null,
            Street = "Moskovska 45/1",
            IsDraft = true,
            UpdatedAt = DateTime.Now,
            RequiredPosition = ".net",

            LevelLanguages = new List<LevelLanguage>
            {
                new()
                {
                    LanguageId = languages[0].Id,
                    LanguageLevel = LanguageLevel.Intermediate
                },
                new()
                {
                    LanguageId = languages[1].Id,
                    LanguageLevel = LanguageLevel.Intermediate
                }
            },

            LevelSkills = new List<LevelSkill>
            {
                new()
                {
                    SkillId = skills[0].Id,
                    SkillLevel = SkillLevel.Advanced
                },
                new()
                {
                    SkillId = skills[1].Id,
                    SkillLevel = SkillLevel.Basic
                }
            },

            Educations = new List<Education>
            {
                new()
                {
                    Degree = "Degree",
                    CreatedAt = DateTime.Now,
                    Description = "Computer science",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    InstitutionName = "Hnu",
                    Specialization = "Specialization",
                    UpdatedAt = DateTime.Now
                }
            },

            Experiences = new List<Experience>
            {
                new()
                {
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    EndDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    Company = "Company",
                    Description = "Description",
                    Position = "Position"
                }
            }
        };

        await context.AddAsync(cv);

        await context.SaveChangesAsync();
    }
}