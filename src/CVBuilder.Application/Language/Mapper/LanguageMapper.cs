using CVBuilder.Application.Language.Responses;

namespace CVBuilder.Application.Language.Mapper;

using Models.Entities;

public class LanguageMapper : AppMapperBase
{
    public LanguageMapper()
    {
        CreateMap<Language, LanguageResult>()
            .ForMember(x => x.LanguageId, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.LanguageName, opt => opt.MapFrom(x => x.Name));
    }
}