using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace WebShopApp.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message, string fromName, bool IsForAccountAction)
        {
            var mimeMessage = new MimeMessage();
            if (IsForAccountAction)
            {
                mimeMessage.From.Add(new MailboxAddress(fromName, "account@tomasospizzeria.com"));
                mimeMessage.To.Add(new MailboxAddress(email));
            }
            else
            {
                mimeMessage.From.Add(new MailboxAddress((fromName != null) ? fromName : "Beställning", email));
                mimeMessage.To.Add(new MailboxAddress("Tomasos pizzeria", "kontakt@emilsodergren.se"));
            }

            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync("smtp01.binero.se", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync("kontakt@emilsodergren.se", "Emil1100");
                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
