namespace CVBuilder.Application.Data.Responses;

public class FileResult
{
    public int Id { get; set; }
    public string Path { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; } 
    public string Url { get; set; }
}