using System.Threading;
using System.Threading.Tasks;
using CVBuilder.Application.User.Queries;
using CVBuilder.Application.User.Responses;
using CVBuilder.Repository;
using MediatR;

namespace CVBuilder.Application.User.Handlers;

using Models;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IRepository<Models.Entities.User, int> _userRepository;

    public GetUserByIdHandler(IRepository<Models.Entities.User, int> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        var response = new UserResponse
        {
            Id = user.Id,
            FirstName = user.IdentityUser.FirstName,
            LastName = user.IdentityUser.LastName,
            PhoneNumber = user.IdentityUser.PhoneNumber,
            Email = user.IdentityUser.Email,
            Site = user.Site,
            CreatedAt = user.CreatedAt,
            AvailabilityStatus = user.AvailabilityStatus,
            AvailabilityStatusDate = user.AvailabilityStatusDate,
            DateInterval = user.DateInterval
        };

        return response;
    }
}