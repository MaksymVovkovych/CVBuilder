using System;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.User.Commands;
using CVBuilder.Application.User.Responses;
using CVBuilder.Models.Entities;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.User.Handlers;

public class GenerateUserAccessTokenHandler : IRequestHandler<CreateUserAccessTokenCommand, UserAccessTokenResult>
{
    private readonly IRepository<AccessToken, int> _accessTokenRepository;

    public GenerateUserAccessTokenHandler(IRepository<AccessToken, int> accessTokenRepository)
    {
        _accessTokenRepository = accessTokenRepository;
    }

    public async Task<UserAccessTokenResult> Handle(CreateUserAccessTokenCommand request,
        CancellationToken cancellationToken)
    {
        var accessToken = new AccessToken
        {
            UserId = request.UserId,
            Token = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        if (request.ExpireDays.HasValue) accessToken.ExpiryAt = DateTime.UtcNow.AddDays(request.ExpireDays.Value);

        var result = await _accessTokenRepository.CreateAsync(accessToken);

        return new UserAccessTokenResult {Token = result.Token};
    }
}