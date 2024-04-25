using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVBuilder.Application.Identity.Services.Interfaces;
using CVBuilder.Application.User.Commands;
using CVBuilder.Application.User.Manager;
using CVBuilder.Application.User.Responses;
using CVBuilder.Models;
using CVBuilder.Models.Entities;
using CVBuilder.Models.Exceptions;
using MediatR;

namespace CVBuilder.Application.User.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;
    private readonly IShortUrlService _shortUrlService;
    private readonly IAppUserManager _userManager;

    public CreateUserHandler(IMediator mediator, IAppUserManager userManager, IIdentityService identityService,
        IMapper mapper, IShortUrlService shortUrlService)
    {
        _mediator = mediator;
        _userManager = userManager;
        _identityService = identityService;
        _mapper = mapper;
        _shortUrlService = shortUrlService;
    }

    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existedUser = await _userManager.FindByEmailAsync(request.Email);
        if (existedUser != null) throw new ForbiddenException("User already exists");

        var identityUser = new IdentityUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
        };
        
        var user = new Models.Entities.User
        {
            IdentityUser = identityUser,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var createdUser = await _userManager.CreateAsync(user, request.Password);
        if (!createdUser.Succeeded)
        {
            var message = string.Join(Environment.NewLine, createdUser.Errors.Select(x => x.Description));
            throw new Exception(message);
        }


        var addRole = await _userManager.AddToRolesAsync(user, new List<string>
        {
            RoleTypes.User.ToString()
        });
        if (!addRole.Succeeded)
        {
            var message = string.Join(Environment.NewLine, addRole.Errors.Select(x => x.Description));
            throw new Exception(message);
        }

        user.ShortUrl = new ShortUrl
        {
            Url = _shortUrlService.GenerateShortUrl()
        };

        var userResult = _mapper.Map<UserResponse>(user);
        return userResult;
    }
}