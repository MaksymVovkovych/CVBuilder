using CVBuilder.Application.Education.Commands;
using CVBuilder.Application.Education.Responses;

namespace CVBuilder.Application.Education.Mappers;

internal class EducationMapper : AppMapperBase
{
    public EducationMapper()
    {
        CreateMap<Models.Entities.Education, CreateEducationCommand>().ReverseMap();

        //CreateMap<CVBuilder.Models.Entities.Education, GetEducationByIdComand>().ReverseMap();
        CreateMap<Models.Entities.Education, EducationByIdResult>().ReverseMap();
        CreateMap<Models.Entities.Education, CreateEducationResult>();
    }
}