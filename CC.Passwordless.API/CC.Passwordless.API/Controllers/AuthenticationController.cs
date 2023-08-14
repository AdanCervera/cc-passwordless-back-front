using CC.Passwordless.API.Models.Request;
using CC.Passwordless.API.Models.Response;
using CC.Passwordless.API.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CC.Passwordless.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService) {
        _authenticationService = authenticationService;
        }
        [HttpPost("login")]
        public Task<AuthenticationResponse<bool>> Get([FromBody] LoginRequest login) =>  _authenticationService.Login(login.Email);

        [Authorize]
        [HttpGet("get-profile")]
        public async Task<bool> GetProfile()
        {
            //Simulating the loading of user profile information.
            await Task.Delay(20000);
            return await Task.FromResult(true);
        }

    }
}
