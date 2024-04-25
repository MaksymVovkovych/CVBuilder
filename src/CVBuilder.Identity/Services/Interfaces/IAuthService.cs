namespace CVBuilder.Identity.Services.Interfaces;

public interface IAuthService
{
    Task GoogleLogin();
    
    Task SignOn();
    //Task LoginShortByUrl();
    Task Logout();
    
    Task SingIn();
    
}