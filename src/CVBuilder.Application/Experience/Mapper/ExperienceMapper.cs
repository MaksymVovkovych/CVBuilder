using System;
using CVBuilder.Application.Experience.Commands;
using CVBuilder.Application.Experience.Responses;

namespace CVBuilder.Application.Experience.Mapper;

public class ExperienceMapper : AppMapperBase
{
    public ExperienceMapper()
    {
        CreateMap<CreateExperienceCommand, Models.Entities.Experience>();

        CreateMap<Models.Entities.Experience, ExperienceResult>();

        CreateMap<Models.Entities.Experience, GetExperienceByIdResult>().ReverseMap();

        CreateMap<Exception, CreateExperienceResult>();
    }
}