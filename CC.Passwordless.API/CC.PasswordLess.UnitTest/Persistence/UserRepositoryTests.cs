using CC.Passwordless.API.Persistence.Implementation;
using CC.Passwordless.API.Persistence.Abstractions;
using CC.Passwordless.Exceptions.Authentication;
using CC.Passwordless.API.Utils.Files.JSonFiles;
using Moq;
using CC.Passwordless.API.Models;

namespace CC.PasswordLess.UnitTest.Persistence
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task IsEmailExists_ValidEmail_ReturnsTrue()
        {
            var expectedEmails = new EmailData
            {
                Emails = new (){ "valid@email.com", "valid2@email.com" }
            };
            var mockJsonFiles = new Mock<IJsonFiles<EmailData>>();
            mockJsonFiles.Setup(f => f.ReadFileToObject(It.IsAny<string>())).ReturnsAsync(expectedEmails);
            IUserRepository userRepository = new UserRepository(mockJsonFiles.Object);
            var result = await userRepository.IsEmailExists("valid@email.com");
            Assert.True(result);
        }

        [Fact]
        public async Task IsEmailExists_InvalidEmail_ReturnsFalse()
        {
            var expectedEmails = new EmailData
            {
                Emails = new() { "valid@email.com", "valid2@email.com" }
            };
            var mockJsonFiles = new Mock<IJsonFiles<EmailData>>();
            mockJsonFiles.Setup(f => f.ReadFileToObject(It.IsAny<string>())).ReturnsAsync(expectedEmails);
            IUserRepository userRepository = new UserRepository(mockJsonFiles.Object);
            var result = await userRepository.IsEmailExists("invalid@gmail.com");
            Assert.False(result);
        }

        [Fact]
        public async Task IsEmailExists_NullEmail_ThrowsNoEmailFoundException()
        {
            var expectedEmails = new EmailData
            {
                Emails = new() { "valid@email.com", "valid2@email.com" }
            };
            var mockJsonFiles = new Mock<IJsonFiles<EmailData>>();
            mockJsonFiles.Setup(f => f.ReadFileToObject(It.IsAny<string>())).ReturnsAsync(expectedEmails);
            IUserRepository userRepository = new UserRepository(mockJsonFiles.Object);
            await Assert.ThrowsAsync<NoEmailFoundException>(() => userRepository.IsEmailExists(email: null));
        }
    }
}
