namespace CVBuilder.Web.Contracts.V1.Requests.Resume;

public class UpdateResumeTemplateRequest
{
    public int TemplateId { get; set; }
    public string TemplateName { get; set; }
    public string Html { get; set; }
    public string EditHtml { get; set; }
}