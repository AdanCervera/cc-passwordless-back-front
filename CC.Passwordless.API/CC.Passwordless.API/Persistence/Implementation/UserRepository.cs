using CC.Passwordless.API.Models;
using CC.Passwordless.API.Persistence.Abstractions;
using CC.Passwordless.API.Utils.Files.JSonFiles;
using CC.Passwordless.Exceptions.Authentication;

namespace CC.Passwordless.API.Persistence.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IJsonFiles<EmailData> _emailFiles;
        private const string path = @"Dummy\data-dummy.json";

        public UserRepository(IJsonFiles<EmailData> emailFiles)
        {
            _emailFiles = emailFiles;
        }

        public async Task<bool> IsEmailExists(string? email)
        {
            EmailData emails = await _emailFiles.ReadFileToObject(path);

            if (email != null && emails?.Emails != null)
            {
                return emails.Emails.Contains(email);
            }
            else
            {
                throw new NoEmailFoundException("This user is not registered.");
            }
        }


    }
}
