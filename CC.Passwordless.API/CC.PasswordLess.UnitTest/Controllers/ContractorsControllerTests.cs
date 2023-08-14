using CC.Passwordless.API.Controllers;
using CC.Passwordless.API.Models;
using CC.Passwordless.API.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace CC.PasswordLess.UnitTest.Controllers
{
    public class ContractorsControllerTests
    {
        [Fact]
        public async Task Get_Authorized_ReturnsContracts()
        {
            var expectedContracts = new List<Contract>
            {
                new Contract { Id = 1, Name = "Contract A", Address = "123 Main St", Budget = 1000.00 },
                new Contract { Id = 2, Name = "Contract B", Address = "456 Elm St", Budget = 1500.00 }
            };

            var mockService = new Mock<IContractsService>();
            mockService.Setup(service => service.GetContracts()).ReturnsAsync(expectedContracts);

            var controller = new ContractorsController(mockService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.Email, "test@example.com"),
            }, "mock"));

            var result = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedContracts = Assert.IsAssignableFrom<IEnumerable<Contract>>(okResult.Value);
            Assert.Equal(expectedContracts.Count, returnedContracts.Count());
        }


        [Fact]
        public async Task Get_Authorized_ReturnsListOfContracts()
        {
            var expectedContracts = new List<Contract>
                {
                    new Contract { Id = 1, Name = "Contract A", Address = "123 Main St", Budget = 1000.00 },
                    new Contract { Id = 2, Name = "Contract B", Address = "456 Elm St", Budget = 1500.00 }
                };

            var mockContractsService = new Mock<IContractsService>();
            mockContractsService.Setup(service => service.GetContracts()).ReturnsAsync(expectedContracts);
            var controller = new ContractorsController(mockContractsService.Object);

            var result = await controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var contracts = Assert.IsAssignableFrom<IEnumerable<Contract>>(okResult.Value);
            Assert.Equal(expectedContracts.Count, contracts.Count());
        }
    }
}
