using Microsoft.AspNetCore.Identity.UI.Services;

namespace Api.Interfaces.IServices {
    public interface ISendMailService : IEmailSender {
        public Task SendEmailsAsync(ICollection<string> emails, string subject, string htmlMessage);
    }
}