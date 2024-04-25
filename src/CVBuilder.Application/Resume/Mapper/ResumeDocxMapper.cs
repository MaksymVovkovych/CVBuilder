using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using CVBuilder.Application.Resume.Responses.Docx;
using CVBuilder.Models;

namespace CVBuilder.Application.Resume.Mapper;

using Models.Entities;

public class ResumeDocxMapper : AppMapperBase
{
    public ResumeDocxMapper()
    {
        CreateMap<Resume, ResumeDocx>()
            .ForMember(x => x.PositionLevel, y => y.MapFrom(z => GetPositionLevel(z.PositionLevel)))
            .ForMember(x => x.PositionName, y => y.MapFrom(z => z.Position.PositionName))
            .ForMember(x => x.Age, y => y.MapFrom(z => GetAge(z.Birthdate).ToString()))
            .ForMember(x => x.Skills, y => y.MapFrom(z => z.LevelSkills))
            .ForMember(x => x.Picture, y => y.MapFrom(z => z.Picture))
            .ForMember(x => x.Languages, y => y.MapFrom(z => z.LevelLanguages))
            .ForMember(x => x.AboutMe, y => y.MapFrom(z => RemoveHtmlTags(z.AboutMe)));


        CreateMap<Education, EducationDocx>()
            .ForMember(x=>x.StartDate,y=>y.MapFrom(z=>z.StartDate.HasValue ? z.StartDate.Value.ToString("MMMM yyyy", CultureInfo.InvariantCulture) : "now"))
            .ForMember(x=>x.EndDate,y=>y.MapFrom(z=>z.EndDate.HasValue ? z.EndDate.Value.ToString("MMMM yyyy", CultureInfo.InvariantCulture) : "now"))
            .ForMember(x=>x.CountTime, y=>y.MapFrom(z=>GetDifferenceDate(z.StartDate, z.EndDate)))
            .ForMember(x=>x.Description,y=>y.MapFrom(z=>RemoveHtmlTags(z.Description)));
            
        
        CreateMap<Experience, ExperienceDocx>()
            // .ForMember(x => x.Skills, y => y.MapFrom(z => MapExperienceSkills(z.Skills)))
            .ForMember(x=>x.StartDate,y=>y.MapFrom(z=>z.StartDate.HasValue ? z.StartDate.Value.ToString("MMMM yyyy", CultureInfo.InvariantCulture) : "now"))
            .ForMember(x=>x.EndDate,y=>y.MapFrom(z=>z.EndDate.HasValue ? z.EndDate.Value.ToString("MMMM yyyy", CultureInfo.InvariantCulture) : "now"))
            .ForMember(x=>x.CountTime, y=>y.MapFrom(z=>GetDifferenceDate(z.StartDate, z.EndDate)))
            .ForMember(x=>x.Description,y=>y.MapFrom(z=>RemoveHtmlTags(z.Description)));

      
        

        CreateMap<LevelSkill, SkillDocx>()
            .ForMember(x => x.SkillName, y => y.MapFrom(z => z.Skill.Name))
            .ForMember(x=>x.Level,y=>y.MapFrom(z=>GetSkillLevel(z.SkillLevel)));

        CreateMap<LevelLanguage, LanguageDocx>()
            .ForMember(x => x.LanguageName, y => y.MapFrom(z => z.Language.Name))
            .ForMember(x=>x.Level,y=>y.MapFrom(z=>GetLanguageLevel(z.LanguageLevel)));
    }

    private static string RemoveHtmlTags(string html)
    {
        if (html != null)
        {
            return Regex.Replace(html, @"<[^>]*>", string.Empty)
                .Replace("&nbsp", " ").Replace("&amp; "," ");
        }

        return null;
    }

    private static int GetAge(string birthDate)
    {
        var date = DateTime.Parse(birthDate);
        var n = DateTime.Now; // To avoid a race condition around midnight
        var age = n.Year - date.Year;

        if (n.Month < date.Month || (n.Month == date.Month && n.Day < date.Day))
            age--;

        return age;
    }

    private static string GetDifferenceDate(DateTime? startDate, DateTime? endDate)
    {
        if (!startDate.HasValue)
            return null;
        endDate ??= DateTime.UtcNow;
        var difference = endDate.Value - startDate.Value;
        var count = DateTime.MinValue + difference;
        var years = count.Year - 1;
        var months = count.Month - 1;
        
        var str = string.Empty;
        
        str += years > 1 ? $"{years} years " 
            : years == 1 ? $"{years} year " : string.Empty;
        
        str += months > 1 ? $"{months} months " 
            : months == 1 ? $"{months} month " : string.Empty;
        

        return str;
    }

    private static string GetLanguageLevel(LanguageLevel level)
    {
        return level switch
        {
            LanguageLevel.Elementary => "Elementary",
            LanguageLevel.PreIntermediate => "Pre-Intermediate",
            LanguageLevel.Intermediate => "Intermediate",
            LanguageLevel.UpperIntermediate => "Upper-Intermediate",
            LanguageLevel.Proficiency => "Proficiency",
            LanguageLevel.Native => "Native",
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, null)
        };
    }

    private static string GetSkillLevel(SkillLevel level)
    {
        return (int)level switch
        {
            <= 0 => "less than year",
            1 => "1 year",
            > 1 => $"{(int)level} years",
        };
    }

    private static List<string> MapExperienceSkills(IEnumerable<ExperienceSkill> skills)
    {
        skills ??= new List<ExperienceSkill>();
        return skills.Select(x => x.Skill.Name).ToList();
    }


    private static string GetPositionLevel(PositionLevel? positionLevel)
    {
        return positionLevel switch
        {
            PositionLevel.Junior => "Junior",
            PositionLevel.Middle => "Middle",
            PositionLevel.Senior => "Senior",
            PositionLevel.TechLead => "Tech Lead",
            null => string.Empty,
            _ => string.Empty
        };
    }
}