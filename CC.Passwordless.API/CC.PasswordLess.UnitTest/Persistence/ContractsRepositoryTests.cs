using CC.Passwordless.API.Models;
using CC.Passwordless.API.Persistence.Implementation;
using CC.Passwordless.API.Utils.Files.JSonFiles;
using Moq;

namespace CC.PasswordLess.UnitTest.Persistence
{
    public class ContractsRepositoryTests
    {
        [Fact]
        public async Task GetContracts_ShouldReturnListOfContracts()
        {
            var expectedContracts = new List<Contract>
    {
        new Contract { Id = 1, Name = "Contract A", Address = "123 Main St", Budget = 1000.00 },
        new Contract { Id = 2, Name = "Contract B", Address = "456 Elm St", Budget = 1500.00 }
    };

            var mockJsonFiles = new Mock<IJsonFiles<IEnumerable<Contract>>>();
            mockJsonFiles.Setup(f => f.ReadFileToObject(It.IsAny<string>())).ReturnsAsync(expectedContracts);

            var repository = new ContractsRepository(mockJsonFiles.Object);

            var result = await repository.GetContracts();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Contract>>(result);
            Assert.Equal(expectedContracts.Count, result.Count());
        }

    }
}

