namespace CVBuilder.Application.Identity.Services.Interfaces;

public interface IShortUrlService
{
    string GenerateShortUrl(int count = 10);
}