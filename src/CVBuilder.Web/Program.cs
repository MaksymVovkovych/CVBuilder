using System.Threading.Tasks;
using CVBuilder.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace CVBuilder.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = await CreateHostBuilder(args)
            .UseNLog()
            .Build()
            .EnsureDbExistsAsync();

        //start the program, a task will be completed when the host starts
        await host.StartAsync();

        //a task will be completed when shutdown is triggered
        await host.WaitForShutdownAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseDefaultServiceProvider(options =>
            {
                
                options.ValidateScopes = false;
                options.ValidateOnBuild = true;
            })
            .ConfigureWebHostDefaults(webBuilder => webBuilder
                .UseStartup<Startup>())
            .ConfigureLogging((_, logging) =>
            {
                logging.AddNLog("nlog.config");
                logging.AddNLogWeb("nlog.config");
            });
    }
}