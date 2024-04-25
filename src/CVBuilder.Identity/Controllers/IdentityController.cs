using CVBuilder.Application.User.Responses;
using CVBuilder.Identity.Services.Interfaces;
using CVBuilder.Web.Contracts.V1.Requests.Identity;
using CVBuilder.Web.Contracts.V1.Requests.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Identity.Controllers;

[ApiController]
[Route("/api/controller")]
public class IdentityController : BaseAuthApiController
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public IdentityController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }
    
    
    
    [HttpPost("WebLogin")]
    public async Task<IActionResult> WebLogin([FromBody] WebLoginRequest request)
    {
        return Ok();
    }
    
    [HttpPost("GoogleLogin")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        return Ok();
    }
    
    [HttpPost("LoginByURL")]
    public async Task<IActionResult> LoginByUrl(string url)
    {
        return Ok();
    }
    
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        return Ok();
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        return Ok();
    }
    
    [HttpGet("GetCurrentUserByToken")]
    public async Task<IActionResult> GetCurrentUserByToken()
    {
        return Ok();
    }
    
    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        return Ok();
    }
    
    [HttpPost("GenerateToken")]
    public async Task<ActionResult<UserAccessTokenResult>> GenerateToken()
    {
        return Ok();
    }

}