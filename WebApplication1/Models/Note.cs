using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace Notes.Models
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; }

        [Required] 
        public string Title { get; set; }

        public string Content { get; set; }

        public string Label { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public Guid UserId { get; set; }
        public User User { get; set; } 
    }

}
