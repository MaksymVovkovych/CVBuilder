namespace CVBuilder.Application.User.Responses;

public class SmallUserResult
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRoleResult Role { get; set; }
    public string CreatedAt { get; set; }
}