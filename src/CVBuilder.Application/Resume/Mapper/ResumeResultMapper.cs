using System;
using System.Collections.Generic;
using System.Linq;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Application.Resume.Responses.Shared;
using CVBuilder.Application.User.Responses;

namespace CVBuilder.Application.Resume.Mapper;

using Models.Entities;

internal class ResumeResultMapper : AppMapperBase
{
    public ResumeResultMapper()
    {

        CreateMap<Resume, ResumeResult>()
            .ForMember(q => q.Picture, w => w.MapFrom(f => f.Picture))
            .ForMember(d => d.Skills, b => b.MapFrom(s => MapToSkillResult(s.LevelSkills)))
            .ForMember(d => d.Languages, b => b.MapFrom(s => MapToUserLanguageResult(s.LevelLanguages)))
            .ForMember(x => x.ShortUrlIncognito, y => y.MapFrom(z => z.ShortUrlIncognito.Url))
            .ForMember(x => x.ShortUrlIncognitoWithoutLogo, y => y.MapFrom(z => z.ShortUrlIncognitoWithoutLogo.Url))
            .ForMember(x => x.ShortUrlFullResume, y => y.MapFrom(z => z.ShortUrlFullResume.Url))
            .ForMember(x => x.CreatedBy, y => y.MapFrom(z => z.CreatedByUser))
            .ForMember(x => x.Owner, y => y.MapFrom(z => z.Owner))
            .ForMember(x=>x.SalaryRate,y=>y.MapFrom(z=>z.SalaryRateDecimal.GetValueOrDefault()))
            ;
        
        CreateMap<Education, EducationResult>();
        CreateMap<Experience, ExperienceResult>();
        CreateMap<ExperienceSkill, SkillResult>()
            .ForMember(x => x.SkillId, y => y.MapFrom(z => z.SkillId))
            .ForMember(x => x.SkillName, y => y.MapFrom(z => z.Skill.Name));
        
        CreateMap<Resume, ResumeCardResult>()
            .ForMember(x => x.Skills, y => y.MapFrom(x => x.LevelSkills))
            .ForMember(x => x.PositionName, y => y.MapFrom(z => z.Position.PositionName))
            .ForMember(x => x.Clients, y => y.MapFrom(z => GetResumeClients(z)))
            .ForMember(x => x.ShortUrlFullResume, y => y.MapFrom(z => z.ShortUrlFullResume.Url))
            .ForMember(x => x.ShortUrlIncognito, y => y.MapFrom(z => z.ShortUrlIncognito.Url))
            .ForMember(x => x.ShortUrlIncognitoWithoutLogo, y => y.MapFrom(z => z.ShortUrlIncognitoWithoutLogo.Url))
            .ForMember(x => x.CreatedBy, y => y.MapFrom(z => z.CreatedByUser))
            .ForMember(x => x.Owner, y => y.MapFrom(z => z.Owner))
            .ForMember(x => x.AvailabilityStatus, y => y.MapFrom(z => z.Owner.AvailabilityStatus))
            .ForMember(x => x.AvailabilityStatusDate, y => y.MapFrom(z => z.Owner.AvailabilityStatusDate))
            .ForMember(x => x.DateInterval, y => y.MapFrom(z => z.Owner.DateInterval))
            .ForMember(x=>x.SalaryRate,y=>y.MapFrom(z=>z.SalaryRateDecimal.GetValueOrDefault()));

        CreateMap<LevelSkill, LevelSkillResult>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.SkillName, y => y.MapFrom(z => z.Skill.Name));

        CreateMap<ResumeTemplate, ResumeTemplateCardResult>()
            .ForMember(x => x.TemplateId, y => y.MapFrom(z => z.Id));

        CreateMap<User, UserResult>()
            .ForMember(x => x.UserId, y => y.MapFrom(z => z.Id));
    }

    #region Methods

    private static List<LevelLanguageResult> MapToUserLanguageResult(IEnumerable<LevelLanguage> levelLanguages)
    {
        return levelLanguages?.Select(MapToUserLanguage).ToList();

        LevelLanguageResult MapToUserLanguage(LevelLanguage levelLanguage)
        {
            return new LevelLanguageResult
            {
                Id = levelLanguage.Id,
                LanguageId = levelLanguage.LanguageId,
                LanguageName = levelLanguage?.Language?.Name,
                Level = (int) levelLanguage.LanguageLevel,
                Order = levelLanguage.Order
            };
        }
    }

    private static List<LevelSkillResult> MapToSkillResult(IEnumerable<LevelSkill> levelSkill)
    {
        return levelSkill?.Select(MapToSkill).ToList();

        LevelSkillResult MapToSkill(LevelSkill skill)
        {
            return new LevelSkillResult
            {
                Id = skill.Id,
                SkillId = skill.SkillId,
                SkillName = skill?.Skill?.Name,
                Level = (int) skill.SkillLevel,
                Order = skill.Order
            };
        }
    }

    private static List<ResumeClientResult> GetResumeClients(Resume resume)
    {
        var clients = resume?.ProposalResumes?.Select(x => x.Proposal).Select(x => x.Client).DistinctBy(x => x.Id);
        var resultClients = clients?.Select(x => new ResumeClientResult
        {
            FirstName = x.IdentityUser.FirstName,
            LastName = x.IdentityUser.LastName,
            ClientId = x.Id
        });
        return resultClients?.ToList();
    }

    #endregion
}