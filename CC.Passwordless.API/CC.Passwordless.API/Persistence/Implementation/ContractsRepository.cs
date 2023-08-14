using CC.Passwordless.API.Models;
using CC.Passwordless.API.Persistence.Abstractions;
using CC.Passwordless.API.Utils.Files.JSonFiles;

namespace CC.Passwordless.API.Persistence.Implementation
{
    public class ContractsRepository : IContractsRepository
    {
        private readonly IJsonFiles<IEnumerable<Contract>> _contractsFile;
        private const string path = @"Dummy\contractors-dummy.json";

        public ContractsRepository(IJsonFiles<IEnumerable<Contract>> contractsFile)
        {
            _contractsFile = contractsFile;
        }

        public async Task<IEnumerable<Contract>> GetContracts() =>  await _contractsFile.ReadFileToObject(path);   
    }
}
