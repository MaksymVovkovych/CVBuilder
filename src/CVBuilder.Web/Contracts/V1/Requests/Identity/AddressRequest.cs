namespace CVBuilder.Web.Contracts.V1.Requests.Identity;

public class AddressRequest
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Building { get; set; }
    public string Apartment { get; set; }

    //public string Latitude { get; set; }
    //public string Longitude { get; set; }
}