using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class NoteLabelEntity
    {

        public int NoteId { get; set; }
        public NotesEntity Note { get; set; } = null!;

        public int LabelId { get; set; }
        public LabelEntity Label { get; set; } = null!;

    }
}
