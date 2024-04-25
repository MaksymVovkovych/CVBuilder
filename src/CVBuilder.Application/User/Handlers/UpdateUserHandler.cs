using System;
using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.User.Commands;
using CVBuilder.Application.User.Responses;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.User.Handlers;
using Models;
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserResponse>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Models.Entities.User, int> _userRepository;

    public UpdateUserHandler(IRepository<Models.Entities.User, int> userRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null)
            throw new NotFoundException("User not found");

        UpdateUser(user, request);
        

        await _userRepository.UpdateAsync(user);

        
        
        var result = new UserResponse
        {
            Id = user.Id,
            FirstName = user.IdentityUser.FirstName,
            LastName = user.IdentityUser.LastName,
            Email = user.IdentityUser.Email,
            Site = user.Site,
            PhoneNumber = user.IdentityUser.PhoneNumber,
            CreatedAt = user.CreatedAt,
            AvailabilityStatus = user.AvailabilityStatus,
            AvailabilityStatusDate = user.AvailabilityStatusDate,
            DateInterval = user.DateInterval
        };


        return result;
    }

    private static void UpdateUser(Models.Entities.User user, UpdateUserCommand request)
    {
        user.IdentityUser.FirstName = request.FirstName;
        user.IdentityUser.LastName = request.LastName;
        user.IdentityUser.PhoneNumber = request.PhoneNumber;
        user.Site = request.Site;
        user.UpdatedAt = DateTime.Now;
        user.AvailabilityStatus = request.AvailabilityStatus;
        user.AvailabilityStatusDate = request.AvailabilityStatusDate;
        user.DateInterval = request.DateInterval;
    }
}