using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Application.User.Manager;

public interface IAppUserManager
{
    Task<IdentityResult> CreateAsync(Models.Entities.User user, string password);
    Task<IdentityResult> AddToRoleAsync(Models.Entities.User user, string role);
    Task<IdentityResult> AddToRolesAsync(Models.Entities.User user, IEnumerable<string> roles);

    Task<Models.Entities.User> FindByEmailAsync(string email);
    Task<Models.Entities.User> FindByPhoneAsync(string phone);

    Task<Models.Entities.User> FindByIdAsync(string userId);
    Task<Models.Entities.User> FindByIdAsync(int userId);


    Task<string> GeneratePasswordResetTokenAsync(Models.Entities.User user);
    Task<IdentityResult> ResetPasswordAsync(Models.Entities.User user, string token, string newPassword);

    Task<IdentityResult> ChangePasswordAsync(Models.Entities.User user, string currentPassword, string newPassword);
    Task<bool> CheckPasswordAsync(Models.Entities.User user, string password);

    Task<IdentityResult> SetEmailAsync(Models.Entities.User user, string email);
    Task<IdentityResult> ConfirmEmailAsync(Models.Entities.User user, string token);

    Task<IdentityResult> ChangePhoneNumberAsync(Models.Entities.User user, string phoneNumber, string token);

    Task<string> GenerateEmailConfirmationTokenAsync(Models.Entities.User user);
    Task<string> GenerateChangePhoneNumberTokenAsync(Models.Entities.User user, string phoneNumber);

    Task<IList<Claim>> GetClaimsAsync(Models.Entities.User user);
    Task<IList<string>> GetRolesAsync(Models.Entities.User user);
}