using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Web.Contracts.V1.Requests.Identity.Driver;

public class DriverRegistrationRequest
{
    [MaxLength(50)] public string FirstName { get; set; }

    [MaxLength(50)] public string LastName { get; set; }

    public int PersonalCode { get; set; }

    [MaxLength(20)] public string VATCode { get; set; }

    public AddressRequest RegistrationAddress { get; set; }

    public string BankName { get; set; }

    public string AccountNumber { get; set; }

    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    [MaxLength(50)]
    public string Email { get; set; }

    [Phone]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [DataType(DataType.Password)] public string Password { get; set; }
}