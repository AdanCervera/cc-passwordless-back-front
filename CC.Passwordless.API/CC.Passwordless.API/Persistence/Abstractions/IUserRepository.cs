namespace CC.Passwordless.API.Persistence.Abstractions
{
    public interface IUserRepository
    {
        Task<bool> IsEmailExists(string? email);
    }
}
