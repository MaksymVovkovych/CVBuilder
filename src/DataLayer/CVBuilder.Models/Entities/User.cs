using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CVBuilder.Models.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Models.Entities;

public class User : IEntity<int> 
{
    
    public int Id { get; set; }
    
    [Encrypted]
    public string Site { get; set; }
  
    [Encrypted]
    public string Contacts { get; set; }
   
    [Encrypted]
    public string CompanyName { get; set; }
    
    
    public CVBuilder.Models.Entities.IdentityUser  IdentityUser { get; set; }

    [ProtectedPersonalData]
    public  string NormalizedUserName { get; set; }

    public int? ShortUrlId { get; set; }

    public ShortUrl ShortUrl { get; set; }

    [ProtectedPersonalData]
    public  string NormalizedEmail { get; set; }

    public List<Resume> CreatedResumes { get; set; }
    public List<Resume> Resumes { get; set; }
    public List<Proposal> CreatedProposals { get; set; }
    public List<Proposal> ClientProposals { get; set; }
    public ICollection<Role> Roles { get; set; }
    public List<UserRole> UserRoles { get; set; }

    public DateInterval? DateInterval { get; set; }
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public DateTime? AvailabilityStatusDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
}