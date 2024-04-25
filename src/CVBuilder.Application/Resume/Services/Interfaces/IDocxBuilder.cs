using System.Threading.Tasks;
using CVBuilder.Application.Resume.Responses.Docx;

namespace CVBuilder.Application.Resume.Services.Interfaces;

public interface IDocxBuilder
{
    /// <summary>
    ///     Generate docx file stream from template path
    /// </summary>
    public Task<byte[]> BindTemplateAsync(ResumeDocx resume, string templatePath, bool isShowLogoFooter = false);

    /// <summary>
    ///     Generate docx file stream from template as byte array
    /// </summary>
    public Task<byte[]> BindTemplateAsync(ResumeDocx resume, byte[] template, bool isShowLogoFooter = false);
}