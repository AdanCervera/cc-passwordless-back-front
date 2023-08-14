namespace CC.Passwordless.Utils.Notifications;

using CC.Passwordless.Exceptions.Authentication;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

public static class EmailUtility
{
  
    public static void SendEmail(string fromEmail, string pssEmail, string toEmail, string subject, string htmlBody, Dictionary<string, string> replacements = null)
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

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_fromEmail, _password);

                using (MailMessage mailMessage = new MailMessage(_fromEmail, toEmail, subject, string.Empty))
                {
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = htmlBody;

                    smtpClient.Send(mailMessage);
                }
            }
            
        }
        catch
        {
            throw new EmailNotificationException("Error sending the email");
        }
    }
}
