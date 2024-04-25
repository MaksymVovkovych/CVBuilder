namespace CVBuilder.Web.Contracts.V1.Requests.Resume.SharedResumeRequest;

public class UpdateSkillRequest
{
    public int? Id { get; set; }
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
}