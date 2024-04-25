using System;
using System.Linq;
using CVBuilder.Application.Identity.Services.Interfaces;

namespace CVBuilder.Application.Identity.Services;

public class ShortUrlService : IShortUrlService
{
    private static readonly Random Random = new();

    public string GenerateShortUrl(int count = 10)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Range(1, count).Select(_ => chars[Random.Next(chars.Length)]).ToArray());
    }
}