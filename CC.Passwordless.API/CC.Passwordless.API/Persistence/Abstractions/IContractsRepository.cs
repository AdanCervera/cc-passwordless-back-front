

using CC.Passwordless.API.Models;

namespace CC.Passwordless.API.Persistence.Abstractions
{
    public interface IContractsRepository
    {
        Task<IEnumerable<Contract>> GetContracts();
    }
}
