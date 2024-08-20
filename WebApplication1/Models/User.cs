using Notes.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required] 
    public string Username { get; set; }

    
    public string Email { get; set; }

    [Required] 
    public string PasswordHash { get; set; }

    public string FirstName { get; set; } 
    public string LastName { get; set; }  

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; 
    public ICollection<Note> Notes { get; set; }
}
