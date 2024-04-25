namespace CVBuilder.Web.Contracts.V1.Requests.Resume.SharedResumeRequest;

public class CreateSkillRequest
{
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    public int Level { get; set; }
    public int Order { get; set; }
}