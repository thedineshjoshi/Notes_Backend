using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
    public class Label
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<NoteLabel> NoteLabels { get; set; }
    }

}
