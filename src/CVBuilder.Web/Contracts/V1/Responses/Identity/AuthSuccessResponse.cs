using System.Collections.Generic;

namespace CVBuilder.Web.Contracts.V1.Responses.Identity;

public class AuthSuccessResponse
{
    public int UserId { get; set; }
    public string UserEmail { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public IList<string> Roles { get; set; }
}