using CVBuilder.Application.Core.Infrastructure.Interfaces;
using CVBuilder.Application.Core.Settings;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVBuilder.Web;

public class Startup
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    private AppSettings _appSettings;
    private IConfiguration _configuration;
    private IEngine _engine;

    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(_webHostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{_webHostEnvironment.EnvironmentName}.json", reloadOnChange: true,
                optional: true)
            .AddEnvironmentVariables();

        _configuration = configurationBuilder.Build();

        services.AddDataBaseContext(_configuration);
        services.AddEfCoreEncrypt(_configuration);
        services.UseEfCoreCache();
        
        
      


        services.AddSingleton<BrowserExtension>();
        (_engine, _appSettings) = services.ConfigureApplicationServices(_configuration, _webHostEnvironment);
    }

    // Configure the DI container 
    public void ConfigureContainer(IServiceCollection services)
    {
        _engine.RegisterDependencies(services, _appSettings);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ConfigureRequestPipeline();
        // app.StartEngine();
    }
}