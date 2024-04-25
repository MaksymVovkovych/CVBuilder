using System.ComponentModel.DataAnnotations;

namespace CVBuilder.Web.Contracts.V1.Requests.Identity;

public class RegisterRequest
{
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    [MaxLength(50)]
    public string Email { get; set; }

    [DataType(DataType.Password)] public string Password { get; set; }
}