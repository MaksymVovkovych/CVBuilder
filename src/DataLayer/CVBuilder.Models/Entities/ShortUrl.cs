using System;
using System.Collections.Generic;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class ShortUrl : Entity<int>
{
    public string Url { get; set; }

    public List<User> Users { get; set; }
    public List<ProposalResume> ProposalResumes { get; set; }
}