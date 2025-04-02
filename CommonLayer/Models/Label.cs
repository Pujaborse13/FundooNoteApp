using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class Label
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Foreign Key - Each label belongs to a user
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Many-to-Many Relationship with Notes
        public ICollection<NoteEntity> Notes { get; set; } = new List<Note>();





    }
}
