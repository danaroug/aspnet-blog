using Microsoft.AspNetCore.Identity.UI.Services; // IEmailSender interface
using Microsoft.Extensions.Logging;              // Logging
using MimeKit;                                   // Email construction
using MailKit.Net.Smtp;                          // SMTP sending
using System;
using System.Threading.Tasks;

namespace WarbandOfTheSpiritborn.Services
{
    /// <summary>
    /// Sends emails via an SMTP server.
    /// Implements IEmailSender so it can be used by Identity services (e.g., email confirmation).
    /// </summary>
    public class SmtpEmailSender : IEmailSender
    {
        private readonly ILogger<SmtpEmailSender> _logger; // Logs success/failure
        private readonly SmtpOptions _smtpOptions;        // SMTP configuration

        /// <summary>
        /// Constructor receives dependencies via Dependency Injection.
        /// </summary>
        public SmtpEmailSender(ILogger<SmtpEmailSender> logger, SmtpOptions smtpOptions)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _smtpOptions = smtpOptions ?? throw new ArgumentNullException(nameof(smtpOptions));
        }

        /// <summary>
        /// Sends an email asynchronously to a specified recipient.
        /// </summary>
        /// <param name="toEmail">Recipient email address</param>
        /// <param name="subject">Email subject line</param>
        /// <param name="message">Email body (HTML)</param>
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                // Create a new email message object
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_smtpOptions.SenderName, _smtpOptions.SenderEmail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;
                email.Body = new TextPart("html") { Text = message };

                // Connect to the SMTP server and send the message
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_smtpOptions.Server, _smtpOptions.Port, _smtpOptions.UseSsl);
                await smtp.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                _logger.LogInformation($"Email successfully sent to {toEmail}");
            }
            catch (Exception ex)
            {
                // Log errors without exposing sensitive details
                _logger.LogError($"Error sending email to {toEmail}: {ex.Message}");
                throw; // Re-throw for calling code to handle
            }
        }
    }

    /// <summary>
    /// Configuration options for the SMTP server.
    /// These values should come from appsettings.json or secret manager.
    /// </summary>
    public class SmtpOptions
    {
        public string Server { get; set; }      // SMTP server address, e.g., smtp.gmail.com
        public int Port { get; set; }           // Port (587 for TLS, 465 for SSL)
        public bool UseSsl { get; set; }        // Whether to use SSL/TLS
        public string Username { get; set; }    // SMTP login username
        public string Password { get; set; }    // SMTP login password
        public string SenderEmail { get; set; } // Email address appearing as sender
        public string SenderName { get; set; }  // Friendly name for the sender
    }
}



