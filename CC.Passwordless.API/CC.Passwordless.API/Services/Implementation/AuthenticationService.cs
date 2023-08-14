using CC.Passwordless.API.Models.Response;
using CC.Passwordless.API.Persistence.Abstractions;
using CC.Passwordless.API.Services.Abstractions;
using CC.Passwordless.API.Utils.Notifications;
using CC.Passwordless.Exceptions.Authentication;
using CC.Passwordless.Utils.Files;
using CC.Passwordless.Utils.General;

namespace CC.Passwordless.API.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private const string EmailMagicLinkpath = @"EmailTemplates\MagicLink.html";
        public AuthenticationService(IUserRepository repository, IConfiguration configuration, IEmailService emailService)
        {
            _repository = repository;
            _configuration = configuration;
            _emailService = emailService;
        }
        public async Task<AuthenticationResponse<bool>> Login(string email)
        {
            var authenticationResponse = new AuthenticationResponse<bool>();
            try
            {
                var isEmailExists = await _repository.IsEmailExists(email);
                if (isEmailExists)
                {
                    SendEmail(email);
                    authenticationResponse.Data = true;
                }
                else
                {
                    throw new NoEmailFoundException("User is not registered");
                }
                return authenticationResponse;
            }
            catch (Exception ex)
            {
                authenticationResponse.Error = true;
                authenticationResponse.ErrorMessage = ex.Message;
                return authenticationResponse;
            }
            
        }

        private void SendEmail(string email)
        {
            string emailFrom = _configuration["Email:EmailFrom"] ?? throw new InvalidOperationException("EmailFrom configuration is missing.");
            string emailpass = _configuration["Email:ApplicationEmailPassword"] ?? throw new InvalidOperationException("ApplicationEmailPassword configuration is missing.");
            Dictionary<string, string> data = new ()
                    {
                        { "Email", email},
                        { "Token",AuthenticationTokenGenerator.GenerateJwtToken(email, email, _configuration)}
                    };

            _emailService.SendEmail(emailFrom, emailpass, email, "Inicio de Session", HtmlFiles.LoadHtmlFromFile(EmailMagicLinkpath)?? String.Empty, data);
        }
    }
}
