using CC.Passwordless.API.Exceptions.Authentication;
using CC.Passwordless.API.Models;
using CC.Passwordless.API.Persistence.Abstractions;
using CC.Passwordless.API.Services.Implementation;
using Moq;

namespace CC.PasswordLess.UnitTest.Services
{
    public class ContractsServiceTests
    {
        [Fact]
        public async Task GetContracts_NoContracts_ThrowsContractsNotFound()
        {
            var mockRepository = new Mock<IContractsRepository>();
            mockRepository.Setup(repo => repo.GetContracts()).ReturnsAsync(new List<Contract>());

            var service = new ContractsService(mockRepository.Object);

            await Assert.ThrowsAsync<ContractsNotFound>(async () => await service.GetContracts());
        }

        [Fact]
        public async Task GetContracts_ContractsFound_ReturnsContracts()
        {
            var expectedContracts = new List<Contract>
            {
                new Contract { Id = 1, Name = "Contract A", Address = "123 Main St", Budget = 1000.00 },
                new Contract { Id = 2, Name = "Contract B", Address = "456 Elm St", Budget = 1500.00 }
            };

            var mockRepository = new Mock<IContractsRepository>();
            mockRepository.Setup(repo => repo.GetContracts()).ReturnsAsync(expectedContracts);

            var service = new ContractsService(mockRepository.Object);

            var result = await service.GetContracts();

            Assert.NotNull(result);
            Assert.Equal(expectedContracts.Count, result.Count());
        }
    }
}
