/*using System.Threading.Tasks;
using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.User.Commands;
using CVBuilder.Application.User.Responses;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Contracts.V1.Requests.Identity;
using CVBuilder.Web.Contracts.V1.Requests.User;
using CVBuilder.Web.Contracts.V1.Responses.Identity;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class IdentityController : BaseAuthApiController
{
    /// <summary>
    /// Registration new user
    /// </summary>
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Register)]
    [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthFailedResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = Mapper.Map<RegisterCommand>(request);
        var response = await Mediator.Send(command);
        if (!response.Success) return BadRequest(Mapper.Map<AuthFailedResponse>(response));

        return Ok(Mapper.Map<AuthSuccessResponse>(response));
    }

    /// <summary>
    /// Login user
    /// </summary>
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Login)]
    [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthFailedResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> WebLogin([FromBody] WebLoginRequest request)
    {
        var command = Mapper.Map<WebLoginCommand>(request);
        var response = await Mediator.Send(command);
        if (!response.Success) return BadRequest(Mapper.Map<AuthFailedResponse>(response));

        return Ok(Mapper.Map<AuthSuccessResponse>(response));
    }

    /// <summary>
    /// Login user
    /// </summary>
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.LoginViaGoogle)]
    [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthFailedResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        var command = Mapper.Map<GoogleLoginCommand>(request);
        var response = await Mediator.Send(command);
        if (!response.Success) return BadRequest(Mapper.Map<AuthFailedResponse>(response));

        return Ok(Mapper.Map<AuthSuccessResponse>(response));
    }

    /// <summary>
    /// Login by ShortUrl
    /// </summary>
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.LoginByUrl)]
    [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthFailedResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LoginByUrl(string url)
    {
        var command = new LoginByShortUrlCommand
        {
            ShortUrl = url
        };

        var response = await Mediator.Send(command);
        if (!response.Success) return BadRequest(Mapper.Map<AuthFailedResponse>(response));

        return Ok(Mapper.Map<AuthSuccessResponse>(response));
    }

    /// <summary>
    /// Return user by Token
    /// </summary>
    [HttpGet(ApiRoutes.Identity.GetCurrentUserByToken)]
    [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthFailedResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCurrentUserByToken()
    {
        var command = new GetCurrentUserByTokenCommand
        {
            UserId = LoggedUserId
        };

        var response = await Mediator.Send(command);
        if (!response.Success) return BadRequest(Mapper.Map<AuthFailedResponse>(response));

        return Ok(Mapper.Map<AuthSuccessResponse>(response));
    }

    /// <summary>
    /// Refresh token
    /// </summary>
    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Refresh)]
    [ProducesResponseType(typeof(AuthSuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthFailedResponse), StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var command = Mapper.Map<RefreshTokenCommand>(request);
        var response = await Mediator.Send(command);
        if (!response.Success) return BadRequest(Mapper.Map<AuthFailedResponse>(response));

        return Ok(Mapper.Map<AuthSuccessResponse>(response));
    }

    /// <summary>
    /// Revoke refresh token
    /// </summary>
    [HttpPost(ApiRoutes.Identity.Logout)]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        var command = Mapper.Map<LogoutCommand>(request);
        command.UserId = LoggedUserId;
        var response = await Mediator.Send(command);
        if (!response) return BadRequest();

        return NoContent();
    }


    /// <summary>
    /// Get new access token
    /// </summary>
    [HttpPost(ApiRoutes.Identity.GenerateToken)]
    public async Task<ActionResult<UserAccessTokenResult>> GenerateToken()
    {
        var command = new CreateUserAccessTokenCommand
        {
            UserId = LoggedUserId
        };

        var response = await Mediator.Send(command);

        return Ok(response);
    }
}*/