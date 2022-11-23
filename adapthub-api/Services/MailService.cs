using SendGrid;
using SendGrid.Helpers.Mail;
using MailKit.Net.Smtp;
using MimeKit;
using adapthub_api.ViewModels;
using System.Text;

namespace adapthub_api.Services
{
    public class MailService : IMailService
    {
        private IConfiguration _configuration;
        private readonly EmailConfiguration _emailConfig;

        public MailService(IConfiguration configuration, EmailConfiguration emailConfig)
        {
            _configuration = configuration;
            _emailConfig = emailConfig; 
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            Encoding encoding = Encoding.UTF8;

            var ma = new MailboxAddress(encoding, toEmail, toEmail);

            var message = new Message(ma, subject, content);

            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);

            /*var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("testttt621@gmail.com", "ADH Support");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);*/
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            Encoding encoding = Encoding.UTF8;

            emailMessage.From.Add(new MailboxAddress(encoding, "Adapthub Support", "testttt621@gmail.com"));
            emailMessage.To.Add(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
