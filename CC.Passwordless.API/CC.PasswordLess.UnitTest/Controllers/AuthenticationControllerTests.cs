using CC.Passwordless.API.Controllers;
using CC.Passwordless.API.Models.Response;
using Moq;
using CC.Passwordless.API.Services.Abstractions;
using CC.Passwordless.API.Models.Request;

namespace CC.PasswordLess.UnitTest.Controllers
{
    public class AuthenticationControllerTests
    {
        [Fact]
        public async Task Login_ValidData_ReturnsSuccessResponse()
        {
            var mockAuthService = new Mock<IAuthenticationService>();
            var controller = new AuthenticationController(mockAuthService.Object);

            var email = "test@example.com";
            var login = new LoginRequest { Email = email };
            var expectedResponse = new AuthenticationResponse<bool> { Data = true };

            mockAuthService.Setup(service => service.Login(email)).ReturnsAsync(expectedResponse);

            var result = await controller.Get(login);

            var okResult = Assert.IsType<AuthenticationResponse<bool>>(result);

            Assert.True(okResult.Data);
            Assert.False(okResult.Error);
            Assert.Null(okResult.ErrorMessage);
        }

        [Fact]
        public async Task Login_InvalidData_ReturnsErrorResponse()
        {
            var mockAuthService = new Mock<IAuthenticationService>();
            var controller = new AuthenticationController(mockAuthService.Object);

            var email = "invalid@example.com";
            var login = new LoginRequest { Email = email };
            var expectedResponse = new AuthenticationResponse<bool> { Error = true, ErrorMessage = "Error" };

            mockAuthService.Setup(service => service.Login(email)).ReturnsAsync(expectedResponse);

            var result = await controller.Get(login);

            var okResult = Assert.IsType<AuthenticationResponse<bool>>(result);

            Assert.False(okResult.Data);
            Assert.True(okResult.Error);
            Assert.NotNull(okResult.ErrorMessage);
        }
    }
}
