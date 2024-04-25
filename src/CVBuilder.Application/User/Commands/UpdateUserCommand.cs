using System;
using CVBuilder.Application.User.Responses;
using CVBuilder.Models;
using MediatR;

namespace CVBuilder.Application.User.Commands;

public class UpdateUserCommand : IRequest<UserResponse>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Site { get; set; }
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public DateInterval? DateInterval { get; set; }
    public DateTime? AvailabilityStatusDate { get; set; }
}