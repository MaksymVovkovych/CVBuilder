using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.User.Commands;
using CVBuilder.Application.User.Responses;
using CVBuilder.Models.Exceptions;
using CVBuilder.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Application.User.Handlers;

using Models;

public class UpdateUserStatusHandler: IRequestHandler<UpdateUserStatusCommand, UserResponse>
{
    private readonly IRepository<Models.Entities.User, int> _userRepository;

    public UpdateUserStatusHandler(IRepository<Models.Entities.User, int> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Table.FirstOrDefaultAsync(x=>x.Id == request.UserId, cancellationToken);

        if (user == null)
            throw new NotFoundException("User not found");

        user.AvailabilityStatus = request.AvailabilityStatus;
        user.AvailabilityStatusDate = request.AvailabilityStatusDate;
        user.DateInterval = request.DateInterval;

        user = await _userRepository.UpdateAsync(user);
        
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
}