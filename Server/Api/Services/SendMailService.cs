using System.Collections.Concurrent;
using Api.Interfaces.IServices;
using Api.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Api.Services
{
    public class SendMailService : ISendMailService
    {
        private readonly MailSetting mailSetting;

        private readonly ILogger<SendMailService> logger;

        public SendMailService(IOptions<MailSetting> _mailSetting, ILogger<SendMailService> _logger)
        {
            mailSetting = _mailSetting.Value;
            logger = _logger;
            logger.LogInformation("Create SendMailService");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(mailSetting.DisplayName, mailSetting.Mail);
            message.From.Add(new MailboxAddress(mailSetting.DisplayName, mailSetting.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSetting.Host, mailSetting.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSetting.Mail, mailSetting.Password);
                await smtp.SendAsync(message);
            }
            catch (Exception ex)
            {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(emailsavefile);

                logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                logger.LogError(ex.Message);
            }

            smtp.Disconnect(true);

            logger.LogInformation("send mail to: " + email);

        }

        public async Task SendEmailsAsync(ICollection<string> emails, string subject, string htmlMessage)
        {
            ConcurrentBag<string> receivers = new ConcurrentBag<string>(emails);

            ParallelOptions options = new ParallelOptions {
                MaxDegreeOfParallelism = 10
            };

            await Parallel.ForEachAsync(receivers, options, async (email, token) => {
                await SendEmailAsync(email, subject, htmlMessage);
            });
        }
    }
}