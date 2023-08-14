using CC.Passwordless.API.Persistence.Abstractions;
using CC.Passwordless.API.Services.Implementation;
using CC.Passwordless.API.Utils.Notifications;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CC.PasswordLess.UnitTest.Services
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public async Task Login_ValidEmail_ReturnsSuccessResponse()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["Jwt:SecretKey"]).Returns("MiClaveSecretaHiperSeguraDe128Bits");
            mockConfiguration.Setup(c => c["Email:ApplicationEmailPassword"]).Returns("geynmiiagobfkm9n");
            mockConfiguration.Setup(c => c["Email:EmailFrom"]).Returns("valid@gmail.com");

            var emailService = new Mock<IEmailService>();
            emailService.Setup(c => c.SendEmail(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>()
                ));
            var authenticationService = new AuthenticationService(mockRepository.Object, mockConfiguration.Object, emailService.Object);

            var email = "valid@gmail.com";
            mockRepository.Setup(repo => repo.IsEmailExists(email)).ReturnsAsync(true);

            // Act
            var result = await authenticationService.Login(email);

            // Assert
            Assert.True(result.Data);
            Assert.False(result.Error);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public async Task Login_InvalidEmail_ReturnsErrorResponse()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockConfiguration = new Mock<IConfiguration>();
            var emailService = new Mock<IEmailService>();
            emailService.Setup(c => c.SendEmail(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>()
                ));
            var authenticationService = new AuthenticationService(mockRepository.Object, mockConfiguration.Object, emailService.Object);

            var email = "invalid@gmail.com";
            mockRepository.Setup(repo => repo.IsEmailExists(email)).ReturnsAsync(false);

            // Act
            var result = await authenticationService.Login(email);

            // Assert
            Assert.False(result.Data);
            Assert.True(result.Error);
            Assert.NotNull(result.ErrorMessage);
        }

        [Fact]
        public async Task Login_ExceptionThrown_ReturnsErrorResponse()
        {
            // Arrange
            var mockRepository = new Mock<IUserRepository>();
            var mockConfiguration = new Mock<IConfiguration>();
            var emailService = new Mock<IEmailService>();
            emailService.Setup(c => c.SendEmail(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>()
                ));
            var authenticationService = new AuthenticationService(mockRepository.Object, mockConfiguration.Object, emailService.Object);

            var email = "test@gmail.com";
            mockRepository.Setup(repo => repo.IsEmailExists(email)).ThrowsAsync(new Exception("Some error."));

            // Act
            var result = await authenticationService.Login(email);

            // Assert
            Assert.False(result.Data);
            Assert.True(result.Error);
            Assert.NotNull(result.ErrorMessage);
        }
    }
}
