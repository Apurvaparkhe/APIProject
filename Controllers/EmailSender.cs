using System.Net;
using System.Net.Mail;
 
namespace SendingEmails
{
public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("apurva.p08@outlook.com", "Apurva#1010")
        };
 
        return client.SendMailAsync(
            new MailMessage(from: "apurva.p08@outlook.com",
                            to: email,
                            subject,
                            message
                            ));
    }
}

}