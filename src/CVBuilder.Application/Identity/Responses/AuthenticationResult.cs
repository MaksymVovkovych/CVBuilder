using System.Collections.Generic;

namespace CVBuilder.Application.Identity.Responses;

public class AuthenticationResult
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
    public string UserEmail { get; set; }
    public IList<string> Roles { get; set; }
}