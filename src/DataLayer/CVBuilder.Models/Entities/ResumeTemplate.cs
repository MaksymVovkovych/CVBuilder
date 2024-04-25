using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ResumeTemplate : Entity<int>
{
    public string TemplateName { get; set; }
    public string Html { get; set; }
    public string EditHtml { get; set; }
    public byte[] Docx { get; set; }
}