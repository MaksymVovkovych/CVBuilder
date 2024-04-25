using System;
using CVBuilder.Models;

namespace CVBuilder.Web.Contracts.V1.Requests.User;

public class UpdateUserStatusRequest
{
    public int UserId { get; set; }
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public DateInterval? DateInterval { get; set; }
    public DateTime? AvailabilityStatusDate { get; set; }
}