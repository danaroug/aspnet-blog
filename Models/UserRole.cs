namespace WarbandOfTheSpiritborn.Models
{
    // Join table for many-to-many relationship between Users and Roles
    public class UserRole
    {
        public int UserId { get; set; } // Composite key part 1
        public User User { get; set; } // Navigation to the related User

        public int RoleId { get; set; } // Composite key part 2
        public Role Role { get; set; } // Navigation to the related Role
    }
}

