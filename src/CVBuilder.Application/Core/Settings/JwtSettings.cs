using System;

namespace CVBuilder.Application.Core.Settings;

public class JwtSettings
{
    public string SecretKey { get; set; }

    public string Issuer { get; set; }

    public TimeSpan TokenLifetime { get; set; }
    public TimeSpan RefreshTokenLifetime { get; set; }
}