namespace CVBuilder.Web.Contracts.V1.Requests.Resume.SharedResumeRequest;

public class CreateLanguageRequest
{
    public int LanguageId { get; set; }
    public string LanguageName { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
}