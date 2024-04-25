namespace CVBuilder.Web.Contracts.V1.Requests.User;

public class ChangeUserRoleRequest
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}