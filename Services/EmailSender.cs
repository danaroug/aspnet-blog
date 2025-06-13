using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;


//public class EmailSender : IEmailSender
//{
//    private readonly ILogger _logger;
//    private readonly string _sendGridApiKey;

//    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor, ILogger<EmailSender> logger)
//    {
//        _sendGridApiKey = optionsAccessor.Value.SendGridKey ?? throw new ArgumentNullException(nameof(optionsAccessor.Value.SendGridKey));
//        _logger = logger;
//    }

//    public async Task SendEmailAsync(string toEmail, string subject, string message)
//    {
//        await Execute(_sendGridApiKey, subject, message, toEmail);
//    }

//    public async Task Execute(string apiKey, string subject, string message, string toEmail)
//    {
//        var client = new SendGridClient(apiKey);
//        var msg = new SendGridMessage()
//        {
//            From = new EmailAddress("danaerougkeri@outlook.com", "Password Recovery"),
//            Subject = subject,
//            PlainTextContent = message,
//            HtmlContent = message
//        };
//        msg.AddTo(new EmailAddress(toEmail));

//        // Disable click tracking.
//        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
//        msg.SetClickTracking(false, false);

//        try
//        {
//            var response = await client.SendEmailAsync(msg);
//            _logger.LogInformation(response.IsSuccessStatusCode
//                                   ? $"Email to {toEmail} queued successfully!"
//                                   : $"Failure Email to {toEmail}");
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError($"Error sending email: {ex}");
//            throw;
//        }

//    }
//}



using MailKit.Net.Smtp;
using MimeKit;

public class SmtpEmailSender : IEmailSender
{
    private readonly ILogger<SmtpEmailSender> _logger;

    public SmtpEmailSender(ILogger<SmtpEmailSender> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Warband", "your@email.com")); // Use your verified address
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("html") { Text = htmlMessage };

        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.office365.com", 587, false); // Outlook SMTP settings
            await client.AuthenticateAsync("your@email.com", "yourPassword");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);

            _logger.LogInformation($"Email to {email} sent via SMTP.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"SMTP send failed: {ex.Message}");
            throw;
        }
    }
}
