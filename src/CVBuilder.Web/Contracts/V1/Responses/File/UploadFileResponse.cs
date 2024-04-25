namespace CVBuilder.Web.Contracts.V1.Responses.File;

public class UploadFileResponse
{
    public int? CvId { get; set; }
    public byte[] Data { get; set; }
    public string ContentType { get; set; }
    public string Name { get; set; }
}