using Notes.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UserRegistration
    {
        [Key] 
        public Guid Id { get; set; }

        [Required] 
        public string Username { get; set; }

        [Required] 
        public string Email { get; set; }

        [Required] 
        public string PasswordHash { get; set; }

        public string FirstName { get; set; } 
        public string LastName { get; set; } 
    }
}
