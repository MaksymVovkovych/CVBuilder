namespace CVBuilder.Web.Contracts.V1.Requests.Client;

public class UpdateClientRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Site { get; set; }
    public string Contacts { get; set; }
    public string CompanyName { get; set; }
}