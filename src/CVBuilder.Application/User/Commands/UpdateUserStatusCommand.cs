using System;
using CVBuilder.Application.User.Responses;
using CVBuilder.Models;
using MediatR;

namespace CVBuilder.Application.User.Commands;

public class UpdateUserStatusCommand: IRequest<UserResponse>
{
    public int UserId { get; set; }
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public DateInterval? DateInterval { get; set; }
    public DateTime? AvailabilityStatusDate { get; set; }
}