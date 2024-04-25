using CVBuilder.Application.Caching;
using CVBuilder.Application.Caching.Interfaces;
using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Data.Services;
using CVBuilder.Application.Data.Services.Interfaces;
using CVBuilder.Application.Pipelines;
using CVBuilder.Application.Proposal.Services;
using CVBuilder.Application.Proposal.Services.Interfaces;
using CVBuilder.Application.Resume.Services;
using CVBuilder.Application.Resume.Services.DocxBuilder;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Repository;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Application.Core.Infrastructure;

public class AddDependenciesRegister : IDependencyRegistrar
{
    public int Order => 1;

    public void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
    {
        services.AddScoped<ICVBuilderFileProvider, CVBuilderFileProvider>();

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        services.AddSingleton<ICacheKeyService, CacheKeyService>();
        // services.AddSingleton<ILocker, MemoryCacheManager>();
        // services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
        services.AddHttpClient();

        services.AddScoped<FileService>();
        services.AddScoped<ImageService>();
        services.AddScoped<IIMageCompressService,ImageCompressService>();
        services.AddScoped<ICurrencyService, Privat24CurrencyService>();

        services.AddScoped<IBrowserPdfPrinter, BrowserPdfPrinter>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipe<,>));

        var assembly = typeof(AddDependenciesRegister).Assembly;
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(LibraryEntrypoint).Assembly));


        // Add DocxBuilder services
        services.AddTransient<IDocxBuilder, DocxBuilder>();
        services.AddTransient<DocxBuilderV2>();
    }
}