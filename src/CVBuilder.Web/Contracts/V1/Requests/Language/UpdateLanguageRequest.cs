namespace CVBuilder.Web.Contracts.V1.Requests.Language;

public class UpdateLanguageRequest
{
    public int LanguageId { get; set; }
    public string LanguageName { get; set; }
}