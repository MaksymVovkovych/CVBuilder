using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Web.Contracts.V1.Requests.Identity;

public class MobileLoginRequest
{
    [Phone]
    [DataType(DataType.PhoneNumber)]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }

    [DataType(DataType.Password)] 
    public string Password { get; set; }
}