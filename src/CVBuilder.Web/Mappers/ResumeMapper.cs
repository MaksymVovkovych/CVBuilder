using CVBuilder.Application.Resume.Commands;
using CVBuilder.Application.Resume.Commands.SharedCommands;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Web.Contracts.V1.Requests.Resume;
using CVBuilder.Web.Contracts.V1.Requests.Resume.SharedResumeRequest;
using CVBuilder.Web.Contracts.V1.Responses.CV;

namespace CVBuilder.Web.Mappers;

public class ResumeMapper : MapperBase
{
    public ResumeMapper()
    {
        CreateMap<GetAllResumeCardRequest, GetAllResumeCardQuery>();
        CreateMap<ResumeCardResult, ResumeCardResponse>();

        #region CreateResume

        CreateMap<CreateResumeRequest, CreateResumeCommand>()
            .ForMember(x => x.PositionId, y => y.MapFrom(z => z.Position.PositionId));
        CreateMap<CreateLanguageRequest, CreateLanguageCommand>();
        CreateMap<CreateSkillRequest, CreateSkillCommand>();
        CreateMap<CreateEducationRequest, CreateEducationCommand>();
        CreateMap<CreateExperienceRequest, CreateExperienceCommand>();

        #endregion

        #region UpdateResume

        CreateMap<UpdateResumeRequest, UpdateResumeCommand>()
            .ForMember(x => x.PositionId, y => y.MapFrom(z => z.Position.PositionId));
        CreateMap<UpdateLanguageRequest, UpdateLanguageCommand>();
        CreateMap<UpdateSkillRequest, UpdateSkillCommand>();
        CreateMap<UpdateEducationRequest, UpdateEducationCommand>();
        CreateMap<UpdateExperienceRequest, UpdateExperienceCommand>();
        CreateMap<ExperienceSkillRequest,ExperienceSkillCommand>();

        #endregion


        #region ResumeTemplate

        CreateMap<UpdateResumeTemplateRequest, UpdateResumeTemplateCommand>();

        #endregion
    }
}