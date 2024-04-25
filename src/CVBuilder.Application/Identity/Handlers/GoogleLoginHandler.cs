using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.Identity.Responses;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Manager;
using CVBuilder.Models;
using CVBuilder.Models.Entities;
using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CVBuilder.Application.Identity.Handlers;

public class GoogleLoginHandler : IRequestHandler<GoogleLoginCommand, AuthenticationResult>
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityService _identityService;
    private readonly IShortUrlService _shortUrlService;
    private readonly IAppUserManager _userManager;

    public GoogleLoginHandler(IConfiguration configuration, IAppUserManager userManager,
        IIdentityService identityService, IShortUrlService shortUrlService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _identityService = identityService;
        _shortUrlService = shortUrlService;
    }

    public async Task<AuthenticationResult> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string> {_configuration["Google:ClientId"]}
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

        var user = await _userManager.FindByEmailAsync(payload.Email) ?? await RegisterUser(payload);

        var result = await _identityService.GenerateAuthenticationResultAsync(user);
        return result;
    }

    private async Task<Models.Entities.User> RegisterUser(GoogleJsonWebSignature.Payload payload)
    {
        var identityUser = new IdentityUser
        {
            Email = payload.Email,
            UserName = payload.Email,
        };
        var user = new Models.Entities.User
        {
            IdentityUser = identityUser,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var password = _shortUrlService.GenerateShortUrl(25);

        var createdUser = await _userManager.CreateAsync(user, password);
        var addRole = await _userManager.AddToRolesAsync(user, new List<string>
        {
            RoleTypes.User.ToString()
        });
        user = await _userManager.FindByEmailAsync(user.IdentityUser.Email);
        return user;
    }
}