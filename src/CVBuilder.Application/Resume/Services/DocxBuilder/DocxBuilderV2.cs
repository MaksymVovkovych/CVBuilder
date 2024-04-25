using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using SautinSoft;

namespace CVBuilder.Application.Resume.Services.DocxBuilder;

public class DocxBuilderV2
{
    public async Task<Stream> ConvertPdfToDocx(Stream pdfStream,
        CancellationToken cancellationToken = default)
    {
        var f = new PdfFocus
        {
            WordOptions =
            {
                Format = PdfFocus.CWordOptions.eWordDocument.Docx
            }
        };
        f.OpenPdf(pdfStream);
        
        var docxBytes = f.ToWord();

        var docxStream = new MemoryStream();
        await docxStream.WriteAsync(docxBytes, cancellationToken);
        
        // if (hideWaterMark)
        //     RemoveWaterMark(docxStream);


        return docxStream;
    }

    private static void RemoveWaterMark(Stream docxStream)
    {
        using var doc = WordprocessingDocument.Open(docxStream, true);
        var elements = doc.MainDocumentPart?.Document.Body;
        var paragraphs = elements?.Where(x => x.InnerText.Contains("Have questions? Email us"));

        foreach (var paragraph in paragraphs)
        {
            var xmlWithRoot = "<root>" + paragraph.InnerXml + "</root>";
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlWithRoot);


            var xmlNodeList = xmlDoc.GetElementsByTagName("mc:AlternateContent");
            for(var i = 0; i< xmlNodeList.Count; i++)
            {
                var xmlNode =xmlNodeList[i];
                var parent = xmlNode?.ParentNode;
                if (xmlNode != null) parent?.RemoveChild(xmlNode);

                var outer = xmlDoc.OuterXml.Replace("<root>", string.Empty);
                paragraph.InnerXml = outer.Replace("</root>", string.Empty);
            }
        }
    }
}