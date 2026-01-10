// UserEmailService.cs
namespace WarbandOfTheSpiritborn.Services
{
    using WarbandOfTheSpiritborn.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Identity;
    using System.Net;

    public class UserEmailService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserEmailService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<string> GetUserEmailAsync(string toEmail)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == toEmail);
            return user?.Email;
        }

        public async Task<IdentityResult> RegisterUserAsync(string email, string password, string callbackUrl, IEmailSender emailSender)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                // Additional properties...
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Generate Email Verification Token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Send Email Verification Link
                var verificationLink = $"{callbackUrl}?userId={user.Id}&code={WebUtility.UrlEncode(token)}";
                var emailSubject = "Confirm your email";
                var emailBody = $"Please confirm your account by clicking this link: <a href='{verificationLink}'>link</a>";

                await emailSender.SendEmailAsync(user.Email, emailSubject, emailBody);

                // You might also log the user in automatically or handle other tasks...
            }
            else
            {
                // Handle the case where user creation failed
                Console.WriteLine($"Error creating user: {string.Join(", ", result.Errors)}");
            }

            return result;
        }
    }
}





