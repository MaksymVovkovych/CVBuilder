using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using Microsoft.IdentityModel.Tokens;

namespace CVBuilder.Application.Identity.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IRepository<RefreshToken, int> _refreshTokenRepository;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public TokenService(
        JwtSettings jwtSettings,
        TokenValidationParameters tokenValidationParameters,
        IRepository<RefreshToken, int> refreshTokenRepository)
    {
        _jwtSettings = jwtSettings;
        _tokenValidationParameters = tokenValidationParameters;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public (string, string) GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return (tokenHandler.WriteToken(token), token.Id);
    }

    public async Task<string> GenerateRefreshTokenAsync(int userId, string tokenId)
    {
        var refreshToken = new RefreshToken
        {
            UserId = userId,
            JwtId = tokenId,
            CreatedAt = DateTime.UtcNow,
            ExpiryAt = DateTime.UtcNow.Add(_jwtSettings.RefreshTokenLifetime),
            Token = GenerateRefreshTokenValue()
        };

        await _refreshTokenRepository.CreateAsync(refreshToken);
        return refreshToken.Token;
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var tokenValidationParameters = _tokenValidationParameters.Clone();
            tokenValidationParameters.ValidateLifetime = false;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
            if (IsJwtWithValidSecurityAlgorithm(validatedToken)) return principal;
        }
        catch (Exception)
        {
            // ignored
        }

        return null;
    }

    public async Task<RefreshToken> GetRefreshTokenAsync(string refreshToken)
    {
        return await _refreshTokenRepository
            .GetByFilter(r => r.Token == refreshToken);
    }

    public async Task<RefreshToken> GetRefreshTokenAsync(int userId, string refreshToken)
    {
        return await _refreshTokenRepository
            .GetByFilter(r => r.UserId == userId
                              && r.Token == refreshToken);
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    public async Task RevokeRefreshTokenAsync(int userId, string refreshToken)
    {
        var storedRefreshToken = await GetRefreshTokenAsync(userId, refreshToken);
        RevokeRefreshToken(storedRefreshToken);
        await UpdateRefreshTokenAsync(storedRefreshToken);
    }

    public async Task RevokeRefreshTokensAsync(int userId)
    {
        var storedRefreshTokens = await _refreshTokenRepository
            .GetListAsync(r => r.UserId == userId);

        foreach (var item in storedRefreshTokens) RevokeRefreshToken(item);

        await _refreshTokenRepository.UpdateRangeAsync(storedRefreshTokens);
    }

    private static void RevokeRefreshToken(RefreshToken refreshToken)
    {
        refreshToken.IsRevoked = true;
        refreshToken.UpdatedAt = DateTime.UtcNow;
    }

    private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return validatedToken is JwtSecurityToken jwtSecurityToken
               && jwtSecurityToken.Header.Alg
                   .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
    }

    private static string GenerateRefreshTokenValue()
    {
        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}