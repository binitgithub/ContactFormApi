using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactFormApi.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace ContactFormApi.Services
{
    public class EmailService
    {
        private readonly EmailConfiguration _config;

        public EmailService(EmailConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(ContactFormModel contactForm)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Contact Form", _config.SmtpUsername));
            email.To.Add(new MailboxAddress("Admin", _config.SmtpUsername));
            email.Subject = contactForm.Subject;
            email.Body = new TextPart("plain")
            {
                Text = $"Name: {contactForm.Name}\n" +
                       $"Email: {contactForm.Email}\n" +
                       $"Message: {contactForm.Message}"
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config.SmtpServer, _config.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config.SmtpUsername, _config.SmtpPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}