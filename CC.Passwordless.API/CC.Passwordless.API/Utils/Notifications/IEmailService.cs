namespace CC.Passwordless.API.Utils.Notifications
{
    public interface IEmailService
    {
        void SendEmail(string fromEmail, string pssEmail, string toEmail, string subject, string htmlBody, Dictionary<string, string> replacements);
    }
}
