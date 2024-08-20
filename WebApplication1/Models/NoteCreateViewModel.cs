using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
    public class NoteCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Label { get; set; }

        public string Content { get; set; }

        [Required]
        public Guid UserId { get; set; }

    }
}
