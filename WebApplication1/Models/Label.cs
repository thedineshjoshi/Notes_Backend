using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
    public class Label
    {
        [Key] 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<NoteLabel> NoteLabels { get; set; }
    }

}
