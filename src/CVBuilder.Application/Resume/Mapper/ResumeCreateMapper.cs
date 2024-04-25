using System;
using System.Collections.Generic;
using CVBuilder.Application.Resume.Commands.SharedCommands;
using Microsoft.IdentityModel.Tokens;
using CreateResumeCommand = CVBuilder.Application.Resume.Commands.CreateResumeCommand;

namespace CVBuilder.Application.Resume.Mapper;

using Models.Entities;

public class CreateResumeMapper : AppMapperBase
{
    public CreateResumeMapper()
    {
        CreateMap<CreateResumeCommand, Resume>()
            .ForMember(x => x.LevelSkills, y => y.MapFrom(z => z.Skills))
            .ForMember(x => x.OwnerId, y => y.MapFrom(z => z.UserId))
            .ForMember(x => x.LevelLanguages, y => y.MapFrom(z => z.Languages))
            .ForMember(x => x.Educations, y => y.MapFrom(x => x.Educations))
            .ForMember(x => x.Experiences, y => y.MapFrom(x => x.Experiences));
      
        CreateMap<CreateLanguageCommand, LevelLanguage>()
            .ForMember(x => x.LanguageId, y => y.MapFrom(z => z.LanguageId))
            .ForMember(x => x.LanguageLevel, y => y.MapFrom(z => z.Level))
            .ForMember(x => x.Language, y => y.MapFrom(z =>
                new Language
                {
                    Id = z.LanguageId,
                    Name = z.LanguageName,
                    CreatedAt = DateTime.UtcNow
                }));

        CreateMap<CreateSkillCommand, LevelSkill>()
            .ForMember(x => x.SkillId, y => y.MapFrom(z => z.SkillId))
            .ForMember(x => x.SkillLevel, y => y.MapFrom(z => z.Level))
            .ForMember(x => x.Skill, y => y.MapFrom(z =>
                new Skill
                {
                    Id = z.SkillId,
                    Name = z.SkillName,
                    CreatedAt = DateTime.UtcNow
                }));
        CreateMap<CreateEducationCommand, Education>();
        
        CreateMap<CreateExperienceCommand, Experience>()
            .ForMember(x => x.Skills, y => y.MapFrom(z => MapExperienceSkills(z.Skills)));

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
                }
            };

            if (experienceSkill.SkillId != 0)
                experienceSkill.Skill = null;
            
            skillsList.Add(experienceSkill);
        }

        return skillsList;
    }
}