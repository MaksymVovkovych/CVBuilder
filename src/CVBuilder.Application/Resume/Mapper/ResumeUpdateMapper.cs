using System;
using System.Collections.Generic;
using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Commands.SharedCommands;
using CVBuilder.Models;
using Microsoft.IdentityModel.Tokens;

namespace CVBuilder.Application.Resume.Mapper;

using Models.Entities;

internal class UpdateResumeMapper : AppMapperBase
{
    public UpdateResumeMapper()
    {
        #region Request

        CreateMap<UpdateResumeCommand, Resume>()
            .ForMember(x => x.LevelSkills, y => y.MapFrom(z => z.Skills))
            .ForMember(x => x.LevelLanguages, y => y.MapFrom(z => z.Languages))
            .ForMember(x => x.Educations, y => y.MapFrom(x => x.Educations))
            .ForMember(x => x.Experiences, y => y.MapFrom(x => x.Experiences))
            // .ForMember(x => x.Timestamp, y => y.MapFrom(x => Convert.FromBase64String(x.Timestamp)))
            .ForMember(x => x.LevelSkills, y => y.MapFrom(x => MapSkills(x.Skills)))
            .ForMember(x => x.LevelLanguages, y => y.MapFrom(x => MapLanguages(x.Languages)));
        
        CreateMap<UpdateLanguageCommand, LevelLanguage>()
            .ForMember(x => x.LanguageId, y => y.MapFrom(z => z.LanguageId))
            .ForMember(x => x.LanguageLevel, y => y.MapFrom(z => z.Level))
            .ForMember(x => x.Language, y => y.MapFrom(z =>
                new Language
                {
                    Id = z.LanguageId,
                    Name = z.LanguageName,
                    UpdatedAt = DateTime.UtcNow
                }));
           
        
        CreateMap<UpdateEducationCommand, Education>();
        CreateMap<UpdateExperienceCommand, Experience>()
            .ForMember(x => x.Skills, y => y.MapFrom(z => MapExperienceSkills(z.Skills)));
        CreateMap<ExperienceSkillCommand, ExperienceSkill>();

        #endregion
    }

    private List<LevelLanguage> MapLanguages(List<UpdateLanguageCommand> languages)
    {
        if (languages.IsNullOrEmpty())
            return new List<LevelLanguage>();
        
        var languageList = new List<LevelLanguage>();
        foreach (var language in languages)
        {
            var levelLanguage = new LevelLanguage
            {
                Id = language.Id.GetValueOrDefault(),
                LanguageId = language.LanguageId,
                LanguageLevel = (LanguageLevel)language.Level,
                Language = new Language
                {
                    Id = language.LanguageId,
                    Name = language.LanguageName,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            if (levelLanguage.LanguageLevel != 0)
                levelLanguage.Language = null;
            
            languageList.Add(levelLanguage);
        }

        return languageList;
    }

    private List<LevelSkill> MapSkills(List<UpdateSkillCommand> skills)
    {
        if (skills.IsNullOrEmpty())
            return new List<LevelSkill>();
        
        var skillsList = new List<LevelSkill>();
        foreach (var skill in skills)
        {
            var skillLevel = new LevelSkill
            {
                Id = skill.Id.GetValueOrDefault(),
                SkillId = skill.SkillId,
                SkillLevel = (SkillLevel)skill.Level,
                Skill = new Skill
                {
                    Id = skill.SkillId,
                    Name = skill.SkillName,
                    UpdatedAt = DateTime.UtcNow
                }

            };
            if (skillLevel.SkillId != 0)
                skillLevel.Skill = null;
            skillsList.Add(skillLevel);

        }

        return skillsList;
    }

    private static List<ExperienceSkill> MapExperienceSkills(List<ExperienceSkillCommand> skills)
    {
        if (skills.IsNullOrEmpty())
            return new List<ExperienceSkill>();
        
        var skillsList = new List<ExperienceSkill>(skills.Count);
        foreach (var skill in skills)
        {
            var experienceSkill = new ExperienceSkill
            {
                SkillId = skill.SkillId,
                Skill = new Skill
                {
                    Name = skill.SkillName
                },
                Id = skill.Id.GetValueOrDefault()
            };

            if (experienceSkill.SkillId != 0)
                experienceSkill.Skill = null;
            
            skillsList.Add(experienceSkill);
        }

        return skillsList;
    }
}