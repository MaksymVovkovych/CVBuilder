using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CVBuilder.Application.User.Manager;

public class AppUserManager : UserManager<Models.Entities.User>, IAppUserManager
{
    public AppUserManager(
        IUserStore<Models.Entities.User> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<Models.Entities.User> passwordHasher,
        IEnumerable<IUserValidator<Models.Entities.User>> userValidators,
        IEnumerable<IPasswordValidator<Models.Entities.User>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<Models.Entities.User>> logger)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
            services, logger)
    {
    }

    public async Task<Models.Entities.User> FindByPhoneAsync(string phone)
    {
        return await Users.FirstOrDefaultAsync(r => r.IdentityUser.PhoneNumber == phone);
    }

    public async Task<Models.Entities.User> FindByIdAsync(int userId)
    {
        return await Users.FirstOrDefaultAsync(r => r.Id == userId);
    }
}