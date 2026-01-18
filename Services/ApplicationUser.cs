using Microsoft.AspNetCore.Identity;

namespace WarbandOfTheSpiritborn.Services
{
    /// <summary>
    /// Represents an application user in the system.
    /// Inherits from IdentityUser, so it already includes properties like Id, UserName, Email, etc.
    /// Add any custom fields here if needed.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        // Example of a custom field:
        // public string? DisplayName { get; set; }
    }

    /// <summary>
    /// Holds constants for role names used in the application.
    /// Helps avoid hardcoding role strings across the codebase.
    /// </summary>
    public static class AppRoles
    {
        public const string Admin = "admin";
        public const string User = "user";
        public const string Guest = "guest";
    }
}



