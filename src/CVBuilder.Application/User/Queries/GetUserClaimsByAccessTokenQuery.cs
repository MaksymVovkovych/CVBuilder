using CVBuilder.Application.User.Responses;
using MediatR;

namespace CVBuilder.Application.User.Queries;

public class GetUserClaimsByAccessTokenQuery : IRequest<UserAccessTokenClaimsResult>
{
    public GetUserClaimsByAccessTokenQuery(string accessToken)
    {
        AccessToken = accessToken;
    }

    public string AccessToken { get; }
}