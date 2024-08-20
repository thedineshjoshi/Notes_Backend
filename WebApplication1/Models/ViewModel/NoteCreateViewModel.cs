using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Notes.Models.ViewModel
{
    public class NoteCreateViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Label { get; set; }
        public string Content { get; set; }
        public string BgColor { get; set; }
        public string FontColor { get; set; }

        [Required]
        public Guid UserId { get; set; }

    }
}
