// Required namespaces for logging, configuration, email sending, and general functionality
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using System;
using WarbandOfTheSpiritborn.Models;

namespace WarbandOfTheSpiritborn.Services
{
    // Implementation of IEmailSender using MailKit for sending emails
    public class MailKitEmailSender : IEmailSender
    {
        // Configuration settings for email (SMTP server, credentials, etc.)
        private readonly EmailSettings _emailSettings;

        // Logger for tracking info and errors
        private readonly ILogger<MailKitEmailSender> _logger;

        // Constructor with dependency injection for email settings and logger
        public MailKitEmailSender(IOptions<EmailSettings> emailSettings, ILogger<MailKitEmailSender> logger)
        {
            // Load email settings from appsettings.json or other config
            _emailSettings = emailSettings.Value;
            _logger = logger;

            // Check if environment variables for SMTP credentials are set
            var envUser = Environment.GetEnvironmentVariable("SMTP_USER");
            var envPass = Environment.GetEnvironmentVariable("SMTP_PASS");

            // Override SMTP credentials with environment variables (useful in production or CI/CD)
            if (!string.IsNullOrEmpty(envUser))
                _emailSettings.SmtpUser = envUser;

            if (!string.IsNullOrEmpty(envPass))
                _emailSettings.SmtpPass = envPass;
        }

        // Asynchronously sends an email to the specified address with subject and HTML content
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Create a new email message using MimeKit
            var emailMessage = new MimeMessage();

            // Set sender information
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));

            // Set recipient email address
            emailMessage.To.Add(new MailboxAddress("", email));

            // Set subject
            emailMessage.Subject = subject;

            // Set body content as HTML
            emailMessage.Body = new TextPart("html") { Text = htmlMessage };

            try
            {
                // Create and configure the SMTP client
                using var client = new SmtpClient();

                // Connect to the SMTP server with secure options
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate using SMTP credentials
                await client.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPass);

                // Send the email
                await client.SendAsync(emailMessage);

                // Disconnect from the SMTP server
                await client.DisconnectAsync(true);

                // Log successful email sending
                _logger.LogInformation($"Email sent to {email}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during sending
                _logger.LogError(ex, "Error sending email.");

                // Re-throw the exception to be handled upstream if needed
                throw;
            }
        }
    }
}
