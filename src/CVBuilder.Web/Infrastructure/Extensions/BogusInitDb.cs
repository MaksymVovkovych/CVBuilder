using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using CVBuilder.EFContext;
using CVBuilder.Models;
using CVBuilder.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Web.Infrastructure.Extensions;

public static class BogusInitDb
{
    public static async Task Init(EfDbContext context, IAppUserManager userManager, IShortUrlService shortUrlService)
    {
        if (await context.Users.AnyAsync())
            return;

        var testUsers = new Faker<User>("en")
            .RuleFor(x => x.IdentityUser.Email, y => y.Person.Email)
            .RuleFor(x => x.IdentityUser.UserName, y => y.Person.Email)
            .RuleFor(x => x.IdentityUser.FirstName, y => y.Person.FirstName)
            .RuleFor(x => x.IdentityUser.LastName, y => y.Person.LastName)
            .RuleFor(x => x.CreatedAt, y => DateTime.UtcNow)
            .RuleFor(x => x.UpdatedAt, y => DateTime.UtcNow)
            .RuleFor(x => x.ShortUrl, y => new ShortUrl {Url = shortUrlService.GenerateShortUrl()});
        var users = testUsers.Generate(80);

        foreach (var user in users.Take(20))
        {
            var createdUser = await userManager.CreateAsync(user, "123456");
            var addRole = await userManager.AddToRolesAsync(user, new List<string>
            {
                RoleTypes.User.ToString()
            });
        }

        foreach (var user in users.Skip(20).Take(20))
        {
            var createdUser = await userManager.CreateAsync(user, "123456");
            var addRole = await userManager.AddToRolesAsync(user, new List<string>
            {
                RoleTypes.Admin.ToString()
            });
        }

        foreach (var user in users.Skip(40).Take(20))
        {
            var createdUser = await userManager.CreateAsync(user, "123456");
            var addRole = await userManager.AddToRolesAsync(user, new List<string>
            {
                RoleTypes.Hr.ToString()
            });
        }

        foreach (var user in users.Skip(60).Take(20))
        {
            var createdUser = await userManager.CreateAsync(user, "123456");
            var addRole = await userManager.AddToRolesAsync(user, new List<string>
            {
                RoleTypes.Client.ToString()
            });
        }
    }
}