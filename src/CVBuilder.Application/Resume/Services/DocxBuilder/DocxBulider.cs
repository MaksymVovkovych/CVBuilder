using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Responses.Docx;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Application.Resume.Services.ResumeBuilder.ClassFiledParser;
using CVBuilder.Application.Resume.Services.ResumeBuilder.ClassFiledParser.Interfaces;
using CVBuilder.Repository;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Hosting;
using OpenXmlPowerTools;
using TemplateEngine.Docx;
using File = CVBuilder.Models.Entities.File;

namespace CVBuilder.Application.Resume.Services.DocxBuilder;

public class DocxBuilder : IDocxBuilder
{
    // private readonly string _tempWorkDocPath;
    // private readonly IWebHostEnvironment _webHostEnvironment;
    // private readonly Content _valuesToFill;

    private readonly IClassFieldParser<ResumeDocx> _classParser;
    private readonly IRepository<File, int> _fileRepository;
    private readonly IHttpClientFactory _clientFactory;
    private Content _content;


    public DocxBuilder(IWebHostEnvironment webHostEnvironment,
        IRepository<File, int> fileRepository, IHttpClientFactory clientFactory)
    {
        _classParser = new ResumeFiledParser();
        // _content = new Content();
        // _valuesToFill = new Content();

        // const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        // var rnd = new Random();
        // var rndString = new string(Enumerable.Range(1, 10).Select(_ => chars[rnd.Next(chars.Length)]).ToArray());

        // _webHostEnvironment = webHostEnvironment;
        // _tempWorkDocPath =
        //     Path.Combine(_webHostEnvironment.ContentRootPath, $"Shared\\docx\\{rndString}.docx");
        _fileRepository = fileRepository;
        _clientFactory = clientFactory;
    }

    public async Task<byte[]> BindTemplateAsync(ResumeDocx resume, byte[] template, bool isShowLogoFooter = false)
    {
        _content = new Content();
        using var stream = new MemoryStream();
        await stream.WriteAsync(template);

        await MapResumeAsync(resume, isShowLogoFooter);

        SaveChangesToFile(stream);

        return stream.ToArray();
    }

    public async Task<byte[]> BindTemplateAsync(ResumeDocx resume, string templatePath,
        bool isShowLogoFooter = false)
    {
        var byteArray = await System.IO.File.ReadAllBytesAsync(templatePath);

        return await BindTemplateAsync(resume, byteArray, isShowLogoFooter);
    }

    private async Task MapResumeAsync(ResumeDocx resume, bool isShowLogoFooter = false)
    {
        // if (isShowLogoFooter) await AddFooterAndHeader();

        MapResumeValues(resume);
        await MapPictureAsync(resume);
        MapListValues(resume);
        // MapDiagrams(resume);
    }

    private void MapResumeValues(ResumeDocx resume)
    {
        var dictionary = _classParser.GetFieldsWithValues(resume, "picture", "position");
        foreach (var value in dictionary)
        {
            var fillValue = new FieldContent(value.Key, value.Value ?? string.Empty);
            _content.Fields.Add(fillValue);
        }
    }

    private async Task MapPictureAsync(ResumeDocx resume)
    {
        if (resume.Picture == null)
            return;

        // using var client = _clientFactory.CreateClient();
        // var response = await client.GetAsync(resume.Picture);
        // var pictureBytes = await response.Content.ReadAsByteArrayAsync();
        // var image = new ImageContent("photo", pictureBytes);
        var myClient = new HttpClient(new HttpClientHandler {UseDefaultCredentials = true});
        var response = await myClient.GetAsync(resume.Picture);
        var pictureBytes = await response.Content.ReadAsByteArrayAsync();
        var image = new ImageContent("photo", pictureBytes);

        _content.Images.Add(image);
    }

    private void MapListValues(ResumeDocx resume)
    {
        var lists = _classParser.GetListFieldsWithValues(resume);

        var skipProperties = new HashSet<string>
        {
            "id",
            "languageId",
            "skillId",
            "order",
            "skills"
        };
        foreach (var item in lists)
            if (item.ListValues.Count > 0)
            {
                var block = new RepeatContent(item.ListName);
                foreach (var blockItem in item.ListValues)
                {
                    var blockItems = new List<IContentItem>();
                    foreach (var val in blockItem)
                    {
                        var value = val.Value;
                        if (skipProperties.Contains(val.Key)) continue;
                      
                        blockItems.Add(new FieldContent(val.Key, value ?? string.Empty));
                    }

                    block.AddItem(blockItems.ToArray());
                }

                _content.Repeats.Add(block);
            }
    }

    private void MapDiagrams(ResumeDocx resume)
    {
        using (var wDoc = WordprocessingDocument.Open("stream", true))
        {
            var documentDiagramNameList = wDoc.MainDocumentPart.Document.Body
                .Descendants<SdtElement>()
                .Where(x => x.SdtProperties.GetFirstChild<Tag>() != null &&
                            x.SdtProperties.GetFirstChild<Tag>().Val.InnerText.Contains("Diagram"))
                .Select(x => x.SdtProperties.GetFirstChild<Tag>().Val.InnerText)
                .ToList();

            var modelLists = _classParser.GetListFieldsWithValues(resume);

            foreach (var diagramName in documentDiagramNameList)
            {
                var nameOnModelList = diagramName.Replace("Diagram", string.Empty);

                var modelData = modelLists
                    .Where(x => x.ListName == nameOnModelList)
                    .Select(x => x.ListValues)
                    .FirstOrDefault();

                if (modelData != null)
                {
                    var categoryNames = new List<string> { "Max level" };
                    var values = new List<double> { 5 };
                    foreach (var dataItem in modelData)
                    {
                        var lable = dataItem
                            .FirstOrDefault(x => x.Key.Contains("Name"))
                            .Value;

                        if (!string.IsNullOrEmpty(lable))
                        {
                            var value = dataItem
                                .FirstOrDefault(x => x.Key.Contains("level"))
                                .Value;

                            categoryNames.Add(lable);
                            values.Add(Convert.ToDouble(value));
                        }
                    }

                    var chartData = new ChartData
                    {
                        SeriesNames = new[]
                        {
                            "Values"
                        },
                        CategoryDataType = ChartDataType.String,
                        CategoryNames = categoryNames.ToArray(),
                        Values = new[] { values.ToArray() }
                    };
                    var isUpdate = ChartUpdater.UpdateChart(wDoc, diagramName, chartData);
                }
            }
        }
    }

    private async Task AddFooterAndHeader()
    {
        var footerFile = Path.Combine("_webHostEnvironment.ContentRootPath", "Shared\\docx\\LayoutFooterHeader.docx");

        //// temporary, while do not have file storage ////
        if (!System.IO.File.Exists(footerFile))
        {
            var footer = await _fileRepository.GetByIdAsync(1);
            // if (footer?.Data == null) return;

            // await System.IO.File.WriteAllBytesAsync(footerFile, footer.Data);
        }

        /////

        var footerHeaderSourceDocx = new FileInfo(footerFile);
        var mainDocx = new FileInfo("_tempWorkDocPath");

        var sources = new List<Source>
        {
            new(new WmlDocument(mainDocx.FullName))
                { KeepSections = false, DiscardHeadersAndFootersInKeptSections = true },
            new(new WmlDocument(footerHeaderSourceDocx.FullName)) { KeepSections = true }
        };

        DocumentBuilder.BuildDocument(sources, "_tempWorkDocPath");
    }

    private void SaveChangesToFile(Stream stream)
    {
        using var outputDocument = new TemplateProcessor(stream)
            .SetRemoveContentControls(true).SetNoticeAboutErrors(false);
        outputDocument.FillContent(_content);
        outputDocument.SaveChanges();
    }
}