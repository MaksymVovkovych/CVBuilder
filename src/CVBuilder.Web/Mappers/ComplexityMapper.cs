using CVBuilder.Application.Complexity.Commands;
using CVBuilder.Web.Contracts.V1.Requests.Complexity;

namespace CVBuilder.Web.Mappers;

public class ComplexityMapper : MapperBase
{
    public ComplexityMapper()
    {
        CreateMap<CreateComplexityRequest, CreateComplexityCommand>();
        CreateMap<UpdateComplexityRequest, UpdateComplexityCommand>();
    }
}