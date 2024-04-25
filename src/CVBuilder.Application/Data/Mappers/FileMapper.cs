using CVBuilder.Application.Data.Responses;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Data.Mappers;

public class FileMapper: AppMapperBase
{
    public FileMapper()
    {
        CreateMap<File, FileResult>();
    }
}