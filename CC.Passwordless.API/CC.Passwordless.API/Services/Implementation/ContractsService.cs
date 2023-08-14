using CC.Passwordless.API.Exceptions.Authentication;
using CC.Passwordless.API.Models;
using CC.Passwordless.API.Persistence.Abstractions;
using CC.Passwordless.API.Services.Abstractions;

namespace CC.Passwordless.API.Services.Implementation
{
    public class ContractsService : IContractsService
    {
        private readonly IContractsRepository _contractRepository;

        public ContractsService(IContractsRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<IEnumerable<Contract>> GetContracts()
        {
            var contractors = await _contractRepository.GetContracts();
            if(contractors == null || !contractors.Any())
            {
                throw new ContractsNotFound("No contracts found");
            }
            return contractors;
        }
    }
}
