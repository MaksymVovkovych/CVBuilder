using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CVBuilder.Models.Entities.Interfaces;

namespace CVBuilder.Models.Entities;

public class Resume : Entity<int>
{
    public int? OwnerId { get; set; }
    public User Owner { get; set; }
    public int? CreatedByUserId { get; set; }
    
    [Encrypted]
    public string Picture { get; set; }
    public User CreatedByUser { get; set; }
    public int? UpdatedByUserId { get; set; }
    public User UpdatedByUser { get; set; }
    public bool IsDraft { get; set; }

    public Position Position { get; set; }
    public int? PositionId { get; set; }
    public PositionLevel? PositionLevel { get; set; }
    public int? ResumeTemplateId { get; set; }
    public ResumeTemplate ResumeTemplate { get; set; }
    
    [Encrypted]
    public string ResumeName { get; set; }
    
    [Encrypted]
    public string FirstName { get; set; }
    
    [Encrypted]
    public string LastName { get; set; }
    
    [Encrypted]
    public string Email { get; set; }
    
    [Encrypted]
    public string Site { get; set; }
    
    [Encrypted]
    public string Phone { get; set; }
 
    [Encrypted]
    public string Code { get; set; }
    
    [Encrypted]
    public string Country { get; set; }
    
    [Encrypted]
    public string City { get; set; }
   
    [Encrypted]
    public string Street { get; set; }
    
    [Encrypted]
    public string RequiredPosition { get; set; }
     [Encrypted]
    public string Birthdate { get; set; }
    
    [Encrypted]
    public string AboutMe { get; set; }
    public int? ShortUrlFullResumeId { get; set; }
    public ShortUrl ShortUrlFullResume { get; set; }
    public int? ShortUrlIncognitoId { get; set; }
    public ShortUrl ShortUrlIncognito { get; set; }
    public int? ShortUrlIncognitoWithoutLogoId { get; set; }
    public ShortUrl ShortUrlIncognitoWithoutLogo { get; set; }
    public List<Education> Educations { get; set; }
    public List<Experience> Experiences { get; set; }
    public List<LevelLanguage> LevelLanguages { get; set; }
    public List<LevelSkill> LevelSkills { get; set; }
    
    [NotMapped]
    public decimal? SalaryRateDecimal
    {
        get => CustomConverter.ToDecimal(SalaryRate);
        set => SalaryRate = value.ToString();
    }

    [Encrypted]
    public string SalaryRate { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
    public int? CountDaysUnavailable { get; set; }
    public List<ProposalResume> ProposalResumes { get; set; }
    
    [Encrypted]
    public string Hobbies { get; set; }
}