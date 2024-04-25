
namespace CVBuilder.Models.Entities;

public class IdentityUser
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public virtual string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    
    public string Password { get; set; }
}