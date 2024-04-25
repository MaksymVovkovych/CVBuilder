using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace CVBuilder.Application.Resume.Services.Interfaces;

public interface IBrowserPdfPrinter
{
    Task<IPage> LoadPageAsync(string url, string resumeJson,string templateHtml);
    Task<Stream> PrintPdfAsync();
}