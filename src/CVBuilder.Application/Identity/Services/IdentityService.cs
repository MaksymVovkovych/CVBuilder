using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using CVBuilder.Application.Identity.Responses;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Application.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly RoleManager<Models.Entities.Role> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly IAppUserManager _userManager;

    public IdentityService(
        IAppUserManager userManager,
        RoleManager<Models.Entities.Role> roleManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    public async Task<AuthenticationResult> GenerateAuthenticationResultAsync(Models.Entities.User user)
    {
        var claims = await GetUserClaims(user);

        var (token, tokenId) = _tokenService.GenerateAccessToken(claims);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user.Id, tokenId);

        return new AuthenticationResult
        {
            UserId = user.Id,
            Success = true,
            Token = token,
            RefreshToken = refreshToken,
            UserEmail = user.IdentityUser.Email,
            Roles = await _userManager.GetRolesAsync(user)
        };
    }

    public async Task<IEnumerable<Claim>> GetUserClaims(Models.Entities.User user)
    {
        var claims = new List<Claim>
        {
            //new(JwtRegisteredClaimNames.Sub, user.Email),
            new(ClaimTypes.GivenName, user.IdentityUser.FullName),
            new(ClaimTypes.Email, user.IdentityUser.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Sid, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
            var role = await _roleManager.FindByNameAsync(userRole);
            if (role == null) continue;

            var roleClaims = await _roleManager.GetClaimsAsync(role);
            foreach (var roleClaim in roleClaims)
            {
                if (claims.Contains(roleClaim)) continue;

                claims.Add(roleClaim);
            }
        }

        return claims;
    }
}