using System;
using CVBuilder.Models;

namespace CVBuilder.Web.Contracts.V1.Requests.User;

public class UpdateUserRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Site { get; set; }
    public AvailabilityStatus? AvailabilityStatus { get; set; }
    public DateTime? AvailabilityStatusDate { get; set; }
    public DateInterval? DateInterval { get; set; }

}