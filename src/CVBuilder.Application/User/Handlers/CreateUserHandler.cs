using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    public CreateUserHandler(IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
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



        var userResult = _mapper.Map<UserResponse>(user);
        return userResult;
    }
}
