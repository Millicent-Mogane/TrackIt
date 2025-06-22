using System.ComponentModel.DataAnnotations;

namespace TrackItApp.Models
{
    // Represents a user in the system
    public class User
    {
        // Unique identifier for the user (Primary Key)
        public int Id { get; set; }

        // User's full name (required field)
        [Required]
        public string FullName { get; set; }

        // User's email address (required, must be in valid email format)
        [Required, EmailAddress]
        public string Email { get; set; }

        // Hashed version of the user's password (required)
        [Required]
        public string PasswordHash { get; set; }

        // Role of the user: "Admin" or "User" (required)
        [Required]
        public string Role { get; set; }  // "Admin" or "User"
    }
}
