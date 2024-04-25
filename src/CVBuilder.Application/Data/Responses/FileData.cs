using System.IO;

namespace CVBuilder.Application.Data.Responses;

public class FileData
{
    public Stream FileStream { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
}