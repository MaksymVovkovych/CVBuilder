namespace CVBuilder.Identity.DTOs;

public class IdentityUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; }
    public string Password { get; set; }
}