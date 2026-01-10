using Microsoft.AspNetCore.Identity;

namespace WarbandOfTheSpiritborn.Services
{
    public class ApplicationUser : IdentityUser
    {
        // Add ONLY custom fields here if needed
        // Example:
        // public string? DisplayName { get; set; }
    }

    // Optional: role name constants (NOT an entity)
    public static class AppRoles
    {
        public const string Admin = "admin";
        public const string User = "user";
        public const string Guest = "guest";
    }
}


