using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace CVBuilder.Application.Resume.Commands;

public class BrowserExtension : IAsyncDisposable
{
    private IBrowser _browser;

    public IBrowser Browser => _browser ??= Init();


    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await _browser.DisposeAsync();
    }

    private static IBrowser Init()
    {
        using var browserFetcher = new BrowserFetcher();
         browserFetcher.DownloadAsync().Wait();
        var browser = Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            Args = new[]
            {
                "--no-sandbox",
                "--ignore-certificate-errors",
                "--enable-feature=NetworkService"
            }, IgnoreHTTPSErrors = true
        }).Result;
        return browser;
    }
}
