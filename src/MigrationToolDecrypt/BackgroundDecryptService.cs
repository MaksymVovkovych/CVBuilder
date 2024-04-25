
using Microsoft.EntityFrameworkCore;
using MigrationToolDecrypt.DbContexts;

namespace MigrationToolDecrypt;

public class BackgroundDecryptService : BackgroundService
{

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BackgroundDecryptService(IServiceScopeFactory serviceScopeFactory)
    {

        _serviceScopeFactory = serviceScopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<DecryptData>();

        await service.DecryptEncryptedData();

    }
}