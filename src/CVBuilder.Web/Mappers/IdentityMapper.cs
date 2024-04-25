using CVBuilder.Application.Identity.Commands;
using CVBuilder.Application.Identity.Responses;
using CVBuilder.Web.Contracts.V1.Requests.Identity;
using CVBuilder.Web.Contracts.V1.Requests.User;
using CVBuilder.Web.Contracts.V1.Responses.Identity;

namespace CVBuilder.Web.Mappers;

public class IdentityMapper : MapperBase
{
    public IdentityMapper()
    {
        CreateMap<AuthenticationResult, AuthFailedResponse>();
        CreateMap<AuthenticationResult, AuthSuccessResponse>();


        //CreateMap<DriverRegistrationRequest, DriverRegistrationCommand>();

        CreateMap<WebLoginRequest, WebLoginCommand>();
        CreateMap<RegisterRequest, RegisterCommand>();
        //CreateMap<CompanyRegistrationRequest, CompanyRegistrationCommand>();

        // CreateMap<CustomerRegistrationRequest, CustomerRegistrationCommand>();

        CreateMap<RefreshTokenRequest, RefreshTokenCommand>();
        CreateMap<LogoutRequest, LogoutCommand>();

        //CreateMap<AddressRequest, AddressCommand>();

        CreateMap<GetCurrentUserByTokenRequest, GetCurrentUserByTokenCommand>();
        CreateMap<GoogleLoginRequest, GoogleLoginCommand>();
    }
}