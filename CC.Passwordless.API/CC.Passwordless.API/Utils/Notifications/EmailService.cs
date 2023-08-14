namespace CC.Passwordless.Utils.Notifications;

using CC.Passwordless.API.Utils.Notifications;
using CC.Passwordless.Exceptions.Authentication;
using System.Net;
using System.Net.Mail;

public class EmailService: IEmailService
{
  
    public void SendEmail(string fromEmail, string pssEmail, string toEmail, string subject, string htmlBody, Dictionary<string, string> replacements)
    {
       string _fromEmail = fromEmail;
       string _password = pssEmail;
        try
        {
            if (replacements != null)
            {
                foreach (var replacement in replacements)
                {
                    htmlBody = htmlBody.Replace($"[{replacement.Key}]", replacement.Value);
                }
            }

            using SmtpClient smtpClient = new("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_fromEmail, _password);

            using MailMessage mailMessage = new(_fromEmail, toEmail, subject, string.Empty);
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = htmlBody;

            smtpClient.Send(mailMessage);

        }
        catch
        {
            throw new EmailNotificationException("Error sending the email");
        }
    }
}
