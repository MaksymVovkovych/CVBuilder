using CVBuilder.Application.User.Responses;
using CVBuilder.Models;

namespace CVBuilder.Application.Resume.Responses;

public class ResumeHistoryResult
{
    public int Id { get; set; }
    public int ResumeId { get; set; }
    public string UpdatedAt { get; set; }
    public SmallUserResult UpdatedUser { get; set; }
    public string NewResumeJson { get; set; }
    public string OldResumeJson { get; set; }
    public ResumeHistoryStatus Status { get; set; }
    public int? DuplicatedResumeId { get; set; }
}