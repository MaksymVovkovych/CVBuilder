using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Queries;
using CVBuilder.Application.Resume.Responses;
using CVBuilder.Application.Resume.Responses.Shared;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CVBuilder.Application.Resume.Handlers;

public class GetResumeByUrlHandler : IRequestHandler<GetResumeByUrlQuery, ResumeShortUrlResult>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Models.Entities.Resume, int> _resumeRepository;

    public GetResumeByUrlHandler(IRepository<Models.Entities.Resume, int> resumeRepository, IMediator mediator)
    {
        _resumeRepository = resumeRepository;
        _mediator = mediator;
    }

    public async Task<ResumeShortUrlResult> Handle(GetResumeByUrlQuery request, CancellationToken cancellationToken)
    {
        var resume = await _resumeRepository.Table
            .Include(x => x.ShortUrlFullResume)
            .Include(x => x.ShortUrlIncognito)
            .Include(x => x.ShortUrlIncognitoWithoutLogo)
            .FirstOrDefaultAsync(x =>
                x.ShortUrlFullResume.Url == request.Url ||
                x.ShortUrlIncognito.Url == request.Url ||
                x.ShortUrlIncognitoWithoutLogo.Url == request.Url, cancellationToken);

        if (resume == null)
            throw new NotFoundException("Resume not found");

        var query = new GetResumeByIdQuery
        {
            Id = resume.Id,
            UserId = request.UserId,
            UserRoles = request.UserRoles,
            IsByUrl = true
        };
        var resumeResult = await _mediator.Send(query, cancellationToken);

        if (resume.ShortUrlFullResume.Url == request.Url)
            return new ResumeShortUrlResult
            {
                Resume = resumeResult,
                ShowLogo = false
            };

        var resumeDuplicateJson = JsonSerializer.Serialize(resumeResult);
        var resumeDuplicate = JsonSerializer.Deserialize<ResumeResult>(resumeDuplicateJson);


        HideName(resumeDuplicate);
        HideContacts(resumeDuplicate);

        if (resume.ShortUrlIncognitoWithoutLogo.Url == request.Url)
            return new ResumeShortUrlResult
            {
                Resume = resumeDuplicate,
                ShowLogo = false
            };
        return new ResumeShortUrlResult
        {
            Resume = resumeDuplicate,
            ShowLogo = true
        };
    }

    private static void HideName(ResumeResult resume)
    {
        resume.LastName = resume.LastName[0].ToString();
    }

    private static void HideContacts(ResumeResult resume)
    {
        resume.Country = null;
        resume.City = null;
        resume.Street = null;
        resume.Code = null;
        resume.Email = null;
        resume.Site = null;
        resume.Phone = null;
    }
}