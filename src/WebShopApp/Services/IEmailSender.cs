using System.Threading.Tasks;

namespace WebShopApp.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, string fromName, bool IsForAccountAction);
    }
}
