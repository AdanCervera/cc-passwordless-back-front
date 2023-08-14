using CC.Passwordless.API.Models;

namespace CC.Passwordless.API.Services.Abstractions
{
    public interface IContractsService
    {
        Task<IEnumerable<Contract>> GetContracts();
    }
}
