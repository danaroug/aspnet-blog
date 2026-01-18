using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services; // IEmailSender
using Microsoft.EntityFrameworkCore;
using System.Net; // For URL encoding
using System.Threading.Tasks;
using WarbandOfTheSpiritborn.Data;

namespace WarbandOfTheSpiritborn.Services
{
    /// <summary>
    /// Service that handles user-related operations, including registration and email retrieval.
    /// </summary>
    public class UserEmailService
    {
        private readonly ApplicationDbContext _dbContext;             // Database access
        private readonly UserManager<ApplicationUser> _userManager;   // Identity user management

        /// <summary>
        /// Constructor receives dependencies via Dependency Injection.
        /// </summary>
        public UserEmailService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        /// <summary>
        /// Returns the email of a user if they exist in the database.
        /// </summary>
        /// <param name="toEmail">Email to search for</param>
        public async Task<string> GetUserEmailAsync(string toEmail)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == toEmail);
            return user?.Email; // null if not found
        }

        /// <summary>
        /// Registers a new user and sends a confirmation email.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <param name="callbackUrl">URL for email confirmation</param>
        /// <param name="emailSender">IEmailSender implementation</param>
        public async Task<IdentityResult> RegisterUserAsync(
            string email,
            string password,
            string callbackUrl,
            IEmailSender emailSender)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            // Create user in Identity system
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Generate confirmation token for email verification
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Build verification link
                var verificationLink = $"{callbackUrl}?userId={user.Id}&code={WebUtility.UrlEncode(token)}";

                // Build email content
                var emailSubject = "Confirm your email";
                var emailBody = $"Please confirm your account by clicking this link: <a href='{verificationLink}'>link</a>";

                // Send confirmation email
                await emailSender.SendEmailAsync(user.Email, emailSubject, emailBody);
            }

            // Return result (success or errors)
            return result;
        }
    }
}





