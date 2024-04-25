using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using CVBuilder.Application.Resume.Responses.Docx;
using CVBuilder.Application.Resume.Services.ResumeBuilder.ClassFiledParser;
using CVBuilder.Application.Resume.Services.ResumeBuilder.ClassFiledParser.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CVBuilder.Application.Resume.Services;

public interface ICustomRender
{
    string SectionId { get; }
    string TemplateId { get; }
    void Render(IElement ul, List<Dictionary<string, string>> list);
}

public class CustomRenderLevelsTwo : ICustomRender
{
    private readonly IDocument _document;

    public CustomRenderLevelsTwo(string sectionId, IDocument document)
    {
        SectionId = sectionId;
        _document = document;
    }

    public string SectionId { get; }
    public string TemplateId => "template-two";

    public void Render(IElement ul, List<Dictionary<string, string>> list)
    {
        var templateLi = ul?.Children.FirstOrDefault(x => x.TagName == "LI");

        if (templateLi == null) return;

        if (ul!.ClassList.Contains("template-two"))
        {
            foreach (var item in list)
            {
                var newLi = templateLi.Clone();
                foreach (var val in item)
                {
                    var liContent = newLi.ChildNodes.GetElementsByClassName(val.Key).FirstOrDefault();

                    if (val.Key == "level")
                    {
                        var listLevelElements = new List<INode>();
                        for (var i = 1; i < 6; i++)
                        {
                            var levelHtml = _document.CreateElement("div");
                            levelHtml.SetAttribute("style",
                                i <= int.Parse(val.Value) ? "background: #fe8503" : "background: #ddd");

                            listLevelElements.Add(levelHtml);
                        }

                        liContent?.AppendNodes(listLevelElements.ToArray());
                    }
                    else if (liContent != null)
                    {
                        liContent.TextContent = val.Value;
                    }
                }

                ul.AppendChild(newLi);
            }

            ul.RemoveChild(templateLi);
        }
    }
}

public class CustomRenderLevelsOne : ICustomRender
{
    public CustomRenderLevelsOne(string sectionId)
    {
        SectionId = sectionId;
    }

    public string SectionId { get; }
    public string TemplateId { get; } = "template-one";

    public void Render(IElement ul, List<Dictionary<string, string>> list)
    {
        var templateLi = ul?.Children.FirstOrDefault(x => x.TagName == "LI");

        if (templateLi == null)
            return;

        if (ul!.ClassList.Contains(TemplateId))
        {
            foreach (var item in list)
            {
                var newLi = templateLi.Clone();
                foreach (var val in item)
                {
                    var liContent = newLi.ChildNodes.GetElementsByClassName(val.Key).FirstOrDefault();

                    if (liContent == null)
                        continue;

                    if (val.Key == "level")
                    {
                        var path = liContent.Children.FirstOrDefault(x =>
                            x.TagName == "path" && x.ClassList!.Contains("circle"));

                        if (path == null)
                            continue;

                        path.Attributes.SetNamedItem(new Attr("stroke-dasharray", $"{int.Parse(val.Value) * 20},100"));
                    }
                    else
                    {
                        liContent.TextContent = val.Value;
                    }
                }

                ul.AppendChild(newLi);
            }

            ul.RemoveChild(templateLi);
        }
    }
}

public class ResumeTemplateBuilder
{
    private readonly IClassFieldParser<ResumeDocx> _classParser;
    private readonly string _html;
    private readonly HtmlParser _parser;
    private IHtmlCollection<IElement> _body;
    private IEnumerable<ICustomRender> _customRenders;
    private IDocument _resumeHtml;

    public ResumeTemplateBuilder(string html)
    {
        _html = html;
        _parser = new HtmlParser();
        _classParser = new ResumeFiledParser();
    }

    public async Task<string> BindTemplateAsync(ResumeDocx resume, bool setFooter = true)
    {
        _resumeHtml = await _parser.ParseDocumentAsync(_html);
        _body = _resumeHtml.All;
        _customRenders = new List<ICustomRender>
        {
            new CustomRenderLevelsOne("languages"),
            new CustomRenderLevelsOne("skills"),
            new CustomRenderLevelsTwo("languages", _resumeHtml),
            new CustomRenderLevelsTwo("skills", _resumeHtml)
        };

        MapResume(resume);

        if (setFooter) SetFooter();

        var div = _resumeHtml.CreateElement("div");
        CreateResumeWrapper(div);
        return div.Html();
    }


    private void CreateResumeWrapper(IElement div)
    {
        div.InnerHtml = "<style>#template-resumes{width:210mm;overflow:hidden;}</style>";
        var secondDiv = _resumeHtml.CreateElement("div");
        secondDiv.SetAttribute("id", "template-resumes");
        secondDiv.InnerHtml = _resumeHtml.ToHtml();
        div.AppendChild(secondDiv);
    }

    private void SetFooter()
    {
        const string footer =
            @"<style>.resume-footer{color: white; width: 100%; height: 90px; background: black; display: flex; justify-content: center; align-items: center;}.resume-footer .footer-wrapper{display: flex; gap: 30px; align-items: center;}.resume-footer .footer-wrapper .title{color: #cccccc; font-size: 20px; font-weight: 400; margin: 0;}.resume-footer .footer-wrapper img{height: 100%;}.resume-footer .footer-wrapper .contacts a{color: #e18360; font-size: 20px; text-decoration: none;}.resume-footer .footer-wrapper .contacts .link-wrapper{display: flex; align-items: center; gap: 10px;}</style><div class=""resume-footer""> <div class=""footer-wrapper""> <img src=""https://cvbuilder-front.vercel.app/assets/logo.jpg""> <div class=""contacts""> <p class=""title"">For communication</p><div class=""link-wrapper""> <svg version=""1.1"" id=""Capa_1"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" x=""0px"" y=""0px"" width=""28.027px"" height=""28.028px"" viewBox=""0 0 28.027 28.028"" style=""enable-background:new 0 0 28.027 28.028;"" xml:space=""preserve""> <path fill=""gray"" d=""M17.146,13.426l10.77-5.383c-0.4-1.682-1.91-2.947-3.71-2.947H3.823c-1.799,0-3.311,1.265-3.71,2.947l10.769,5.383C12.502,14.237,15.526,14.237,17.146,13.426z""/> <path fill=""gray"" d=""M17.717,14.565c-0.996,0.499-2.311,0.772-3.703,0.772s-2.707-0.274-3.703-0.772l-2.978-1.489L0,16.743v2.367c0,2.102,1.72,3.821,3.822,3.821h20.383c2.102,0,3.822-1.72,3.822-3.821v-2.367l-7.333-3.666L17.717,14.565z""/> <polygon fill=""gray"" points=""22.119,12.365 28.027,15.319 28.027,9.411 ""/> <polygon fill=""gray"" points=""0.001,9.41 0.001,15.319 5.909,12.365 ""/> </svg> <a href=""mailto:info@ithoot.com"">info@ithoot.com</a> </div><div class=""link-wrapper""> <svg width=""28px"" height=""28px"" viewBox=""0 0 48 48"" fill=""none"" xmlns=""http://www.w3.org/2000/svg""> <path fill=""gray"" d=""M41.4193 7.30899C41.4193 7.30899 45.3046 5.79399 44.9808 9.47328C44.8729 10.9883 43.9016 16.2908 43.1461 22.0262L40.5559 39.0159C40.5559 39.0159 40.3401 41.5048 38.3974 41.9377C36.4547 42.3705 33.5408 40.4227 33.0011 39.9898C32.5694 39.6652 24.9068 34.7955 22.2086 32.4148C21.4531 31.7655 20.5897 30.4669 22.3165 28.9519L33.6487 18.1305C34.9438 16.8319 36.2389 13.8019 30.8426 17.4812L15.7331 27.7616C15.7331 27.7616 14.0063 28.8437 10.7686 27.8698L3.75342 25.7055C3.75342 25.7055 1.16321 24.0823 5.58815 22.459C16.3807 17.3729 29.6555 12.1786 41.4193 7.30899Z""/> </svg> <a href=""https://t.me/IT_HOOT"">Telegram</a> </div></div></div></div>";

        _resumeHtml.Body.InnerHtml += footer;
    }

    private void MapResume(ResumeDocx resume)
    {
        var dictionary = _classParser.GetFieldsWithValues(resume, "picture", "position");
        foreach (var value in dictionary) MapResumeValue(value.Key, value.Value);

        MapPicture(resume);
        MapResumeLists(resume);
    }


    private void MapPicture(ResumeDocx resume)
    {
        var picture = _body.FirstOrDefault(x => x.ClassName == "picture");
        if (resume.Picture != null) picture?.Attributes.SetNamedItem(new Attr("src", resume.Picture));
    }

    private void MapResumeLists(ResumeDocx resume)
    {
        var listFields = _classParser.GetListFieldsWithValues(resume);

        foreach (var listField in listFields) BindList(listField.ListName, listField.ListValues);
    }

    private void BindList(string sectionId, List<Dictionary<string, string>> list)
    {
        var section = _resumeHtml.QuerySelector($"#{sectionId}");
        var ul = section?.QuerySelector("ul");
        var templateLi = ul?.QuerySelector("li");


        if (ul == null || templateLi == null)
            return;

        if (list.IsNullOrEmpty())
        {
            section.Remove();
            return;
        }


        if (ul.ClassList.Contains("default"))
        {
            var renders = _customRenders.Where(x => x.SectionId == sectionId);
            foreach (var render in renders)
                if (ul.ClassList.Contains(render.TemplateId))
                    render.Render(ul, list);

            return;
        }


        foreach (var item in list)
        {
            var newLi = templateLi!.Clone();
            foreach (var val in item)
            {
                var liContent = newLi.ChildNodes.GetElementsByClassName(val.Key).FirstOrDefault();
                if (liContent != null) liContent.TextContent = val.Value;
            }

            ul.AppendChild(newLi);
        }

        ul.RemoveChild(templateLi);
    }

    private void MapResumeValue(string selector, string value)
    {
        if (value == null)
            return;
        var values = _body.Where(x => x.ClassList.Contains(selector));
        foreach (var val in values) val.TextContent = value;
    }
}