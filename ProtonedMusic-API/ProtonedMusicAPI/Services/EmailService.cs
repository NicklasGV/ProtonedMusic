using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using ProtonedMusicAPI.DTO.EmailDTO;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace ProtonedMusicAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions.Value;
        }
        public void SendEMail(EmailResponse mailData)
        {
            using (MimeMessage emailMessage = new MimeMessage())
            {
                MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                emailMessage.From.Add(emailFrom);
                emailMessage.To.Add(MailboxAddress.Parse(mailData.To));

                emailMessage.Subject = mailData.Subject;

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.TextBody = mailData.Body;
                emailMessage.Body = new TextPart(TextFormat.Html)
                {
                    Text = mailData.Body
                };

                using SmtpClient mailClient = new SmtpClient();
                mailClient.Connect(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.StartTls);
                mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                mailClient.Send(emailMessage);
                mailClient.Disconnect(true);
            }
        }
    }

}
