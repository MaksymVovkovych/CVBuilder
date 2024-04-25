using System.IO;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Services.Interfaces;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace CVBuilder.Application.Resume.Services;

public class BrowserPdfPrinter : IBrowserPdfPrinter
{
    private readonly BrowserExtension _browser;
    private IPage _page;

    public BrowserPdfPrinter(BrowserExtension browser)
    {
        _browser = browser;
    }


    public async Task<IPage> LoadPageAsync(string url, string resumeJson, string templateHtml)
    {
        var browser = _browser.Browser;
        _page = await browser.NewPageAsync();
        await _page.SetCacheEnabledAsync();
        await _page.EmulateMediaTypeAsync(MediaType.Print);
        
        if (resumeJson != null)
        {
            await _page.EvaluateExpressionOnNewDocumentAsync($"sessionStorage.setItem('resume',`{resumeJson.Replace(@"\",@"\\")}`);");
        }
        
        if (templateHtml != null)
        {
            await _page.EvaluateExpressionOnNewDocumentAsync($"sessionStorage.setItem('template',`{templateHtml.Replace(@"\",@"\\")}`);");

        }

        
        await _page.GoToAsync(url, WaitUntilNavigation.Networkidle0);
        return _page;
    }


    public async Task<Stream> PrintPdfAsync()
    {
        await _page.WaitForSelectorAsync("#resume-loaded", new WaitForSelectorOptions
        {
            Visible = true,
            Timeout = 5000
        });


        var height = await _page.EvaluateExpressionAsync<int>("document.body.offsetHeight");
        var pdfOptions = new PdfOptions
        {
            PrintBackground = true,
            Height = height,
            Width = "210mm",
            PreferCSSPageSize = true
        };

        var stream = await _page.PdfStreamAsync(pdfOptions);
        return stream;
    }
}