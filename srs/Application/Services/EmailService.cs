using Application.Interfaces;
using Application.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net.Mail;
using Domain.Entity;
using MimeKit;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        private readonly ILogger _logger;

        public EmailService(SMTP configuration, ILogger logger)
        {
            _smtpServer = configuration.SmtpServer;
            _smtpPort = configuration.SmtpPort;
            _smtpUsername = configuration.SmtpUsername;
            _smtpPassword = configuration.SmtpPassword;
            _logger = logger;
        }
        /// <summary>
        /// Receives a model with data to send and mail to send, 
        /// The model is processed, logged to the database and handles errors 
        /// that are also logged to along with the execution status
        /// </summary>
        /// <param name="model">Map model from controller</param>
        public async Task Send(EmailModel model)
        {
            string incorrectEmail = string.Empty;

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Sender", _smtpUsername));

            foreach (var recipient in model.Recipients)
            {
                incorrectEmail = isValidEmail(recipient)
                    ? string.IsNullOrEmpty(incorrectEmail)
                    ? incorrectEmail += $"Invalid email:\t{recipient}" : incorrectEmail += $",\tInvalid email:\t{recipient}" : incorrectEmail += "";
                    email.To.Add(new MailboxAddress("Recipient", recipient));
            }
            email.Subject = model.Subject;
            email.Body = new TextPart("plain") 
            {
                Text = model.Body
            };

            var message = Packer(model);

            try
            {
                await SendMessage(email);
                await _logger.Log(message, incorrectEmail);
            }
            catch (SmtpCommandException ex)
            {
                await _logger.Log(message, incorrectEmail, ex.Message);
            }
            catch (SmtpProtocolException ex)
            {
                await _logger.Log(message, incorrectEmail, ex.Message);
            }
            catch (AuthenticationException ex)
            {
                await _logger.Log(message, incorrectEmail, ex.Message);
            }
            catch (IOException ex)
            {
                await _logger.Log(message, incorrectEmail, ex.Message);
            }
            catch (TimeoutException ex)
            {
                await _logger.Log(message, incorrectEmail, ex.Message);
            }
            catch (Exception ex)
            {
                await _logger.Log(message, incorrectEmail, ex.Message);
            }
        }
        /// <summary>
        /// Setting SMTP Servers,and send Message
        /// </summary>
        /// <param name="message"></param>
        private async Task SendMessage(MimeMessage message)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();        
            await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.SslOnConnect).ConfigureAwait(false);
            await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
            await client.SendAsync(message);
            var option = FormatOptions.Default.Clone();
            if (client.Capabilities.HasFlag(SmtpCapabilities.UTF8))
                option.International = true;
            client.Disconnect(true);
        }
        /// <summary>
        /// Validation
        /// </summary>
        /// <returns></returns>
        private bool isValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <param name="model">Map model</param>
        /// <returns> Model for database</returns>
        private Message Packer(EmailModel model)
        {
            var message = new Message()
            {
                Subject = model.Subject,
                Body = model.Body,
                DateSend = DateTime.UtcNow,
                Recipients = new List<Recipient>()
            };
            foreach (var item in model.Recipients)
                message.Recipients.Add(new Recipient() { Recipients = item, Message = message });

            return message;
        }
    }
}
