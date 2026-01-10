using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Threading.Tasks;
using System;
using WarbandOfTheSpiritborn.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private readonly string _sendGridApiKey;
    private readonly UserEmailService _userEmailService;

    public EmailSender(
        IOptions<AuthMessageSenderOptions> optionsAccessor, ILogger<EmailSender> logger, UserEmailService userEmailService)
    {
        _sendGridApiKey = optionsAccessor.Value.SendGridKey ?? throw new ArgumentNullException(nameof(optionsAccessor.Value.SendGridKey));
        _logger = logger;
        _userEmailService = userEmailService;
    }
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        try
        {
            // Validate email address
            if (!IsValidEmail(toEmail))
            {
                _logger.LogWarning($"Invalid email address: {toEmail}. Unable to send the email.");
                return;
            }

            _logger.LogInformation($"Sending email to: {toEmail}");

            string userEmail = await _userEmailService.GetUserEmailAsync(toEmail);

            if (!string.IsNullOrEmpty(userEmail))
            {
                await Execute(_sendGridApiKey, subject, message, userEmail);
            }
            else
            {
                // Handle the case where userEmail is empty or null
                _logger.LogWarning($"User email not found or is empty for {toEmail}. Unable to send the email.");
            }
        }
        catch (AggregateException ex)
        {
            // Handle exceptions from GetUserEmailAsync
            foreach (var innerEx in ex.InnerExceptions)
            {
                _logger.LogError($"Error in GetUserEmailAsync: {innerEx.Message}");
            }

            // Decide on the appropriate action, e.g., log the error, return a default email, etc.
        }
        catch (InvalidOperationException ex)
        {
            // Handle specific exceptions, if any
            _logger.LogError($"Error: {ex.Message}");
            throw;
        }
    }
    private async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("danaerougkeri@outlook.com", "Password Recovery"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };

        msg.AddTo(new EmailAddress(toEmail));

        msg.SetClickTracking(false, false);

        try
        {
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sending email: {ex}");
            throw;
        }
    }
}



