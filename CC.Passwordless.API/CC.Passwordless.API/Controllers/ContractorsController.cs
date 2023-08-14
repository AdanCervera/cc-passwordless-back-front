using CC.Passwordless.API.Exceptions.Authentication;
using CC.Passwordless.API.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CC.Passwordless.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorsController : ControllerBase
    {
        private readonly IContractsService _ContractsService;

        public ContractorsController(IContractsService contractsService)
        {
            _ContractsService = contractsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var contracts = await _ContractsService.GetContracts();
                return Ok(contracts);
            }
            catch (ContractsNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
