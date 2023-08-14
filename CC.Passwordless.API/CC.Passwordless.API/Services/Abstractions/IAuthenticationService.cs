using CC.Passwordless.API.Models.Response;

namespace CC.Passwordless.API.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse<bool>> Login(string email);
    }
}
