using System.Net.Mail;
using Microsoft.Extensions.Options;
using Net.Architecture.Business.Abstract;
using Net.Architecture.Business.Helpers.Abstract;
using Net.Architecture.Entities.Configurations;
using Net.Architecture.Entities.Views;

namespace Net.Architecture.Business.Helpers.Concrete
{
    public class EmailHelper : BaseBusiness, IEmailHelper
    {
        private readonly EmailSettings _outlookDefaultEmailSettings;

        public EmailHelper(IOptions<EmailSettings> outlookDefaultEmailSettings)
        {
            _outlookDefaultEmailSettings = outlookDefaultEmailSettings.Value;
        }

        public void SendMailWithDefaultOutlook(MailContentView mailContent)
        {
            Send(_outlookDefaultEmailSettings, mailContent);
        }

        private void Send(EmailSettings emailSettings, MailContentView mailContent)
        {
            var mail = new SmtpClient(emailSettings.Server, emailSettings.Port);
            mail.EnableSsl = emailSettings.EnableSsl;
            mail.UseDefaultCredentials = emailSettings.UseDefaultCredentials;
            mail.Credentials = new System.Net.NetworkCredential(emailSettings.Username, emailSettings.Password);

            var message = new MailMessage();
            message.IsBodyHtml = emailSettings.IsBodyHtml;
            message.Priority = emailSettings.Priority;
            message.From = new MailAddress(emailSettings.FromMailAddress);

            foreach (var to in mailContent.ToList)
            {
                var mailTo = new MailAddress(to);
                message.To.Add(mailTo);
            }

            message.Subject = mailContent.Title;
            message.Body = mailContent.Body;
            mail.Send(message);
        }
    }
}
