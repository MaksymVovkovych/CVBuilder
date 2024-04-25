using System;
using System.ComponentModel.DataAnnotations;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ResumeHistory : Entity<int>
{
    public ResumeHistoryStatus Status { get; set; }
    public int ResumeId { get; set; }
    public Resume Resume { get; set; }

    public int? DuplicatedResumeId { get; set; }
    public Resume DuplicatedResume { get; set; }

    public int UpdateUserId { get; set; }
    public User UpdateUser { get; set; }

    [Encrypted]
    public string OldResumeJson { get; set; }
    [Encrypted]
    public string NewResumeJson { get; set; }
}