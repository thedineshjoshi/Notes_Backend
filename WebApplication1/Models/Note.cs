using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace Notes.Models
{
    public class Note
    {        
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Label { get; set; }
        public string BgColor { get; set; }
        public string FontColor { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
        public User User { get; set; } 
    }

}
