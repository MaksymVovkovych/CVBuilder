using System.Collections.Generic;
using System.Security.Claims;

namespace CVBuilder.Application.User.Responses;

public class UserAccessTokenClaimsResult
{
    public bool IsTokenExpired { get; set; }
    public IEnumerable<Claim> Claims { get; set; } = new List<Claim>();
}