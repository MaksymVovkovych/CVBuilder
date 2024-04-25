using System.Linq;
using CVBuilder.Application.Position.Responses;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Application.Skill.DTOs;
using CVBuilder.Models.Entities;

namespace CVBuilder.Application.Proposal.Mapper;

public class GetProposalMapper : AppMapperBase
{
    public GetProposalMapper()
    {
        CreateMap<Models.Entities.Proposal, SmallProposalResult>()
            .ForMember(x => x.ProposalSize, y => y.MapFrom(z => z.Resumes.Count))
            .ForMember(x => x.Positions, y => y.MapFrom(z => z.Resumes
                .Select(x => x.Resume.Position)
                .DistinctBy(x => x.Id)
                .Select(x => new PositionResult
                {
                    PositionId = x.Id,
                    PositionName = x.PositionName
                }).ToList()
            ))
            .ForMember(x => x.StatusProposal, y => y.MapFrom(z => z.StatusProposal.ToString()))
            .ForMember(x => x.LastUpdated, y => y.MapFrom(z => z.UpdatedAt.ToString("MM/dd/yyyy HH:mm:ss UTC")))
            .ForMember(x => x.CreatedUserName, y => y.MapFrom(z => z.CreatedUser.IdentityUser.FullName))
            .ForMember(x => x.ClientUserName, y => y.MapFrom(z => z.Client.IdentityUser.FullName));

        #region Result

        CreateMap<Models.Entities.Proposal, ProposalResult>();
        CreateMap<Models.Entities.User, ProposalClientResult>()
            .ForMember(x => x.UserId, y => y.MapFrom(z => z.Id))
            .ForMember(x => x.ShortAuthUrl, y => y.MapFrom(z => z.ShortUrl.Url));

        CreateMap<ProposalResume, ResumeResult>()
            .ForMember(x => x.ResumeId, y => y.MapFrom(z => z.ResumeId))
            .ForMember(x => x.FirstName, y => y.MapFrom(z => z.Resume.FirstName))
            .ForMember(x => x.LastName, y => y.MapFrom(z => z.Resume.LastName))
            .ForMember(x => x.ResumeName, y => y.MapFrom(z => z.Resume.ResumeName))
            .ForMember(x => x.Skills, y => y.MapFrom(z => z.Resume.LevelSkills))
            .ForMember(x => x.Picture, y => y.MapFrom(z => z.Resume.Picture))
            .ForMember(x => x.PositionId, y => y.MapFrom(z => z.Resume.PositionId))
            .ForMember(x => x.PositionName, y => y.MapFrom(z => z.Resume.Position.PositionName))
            .ForMember(x => x.ShortUrl, y => y.MapFrom(z => z.ShortUrl.Url))
            .ForMember(x => x.SalaryRate, y => y.MapFrom(z => z.Resume.SalaryRateDecimal.GetValueOrDefault()));

        CreateMap<LevelSkill, SkillResult>()
            .ForMember(x => x.SkillId, y => y.MapFrom(z => z.SkillId))
            .ForMember(x => x.SkillName, y => y.MapFrom(z => z.Skill.Name));

        #endregion
    }
}