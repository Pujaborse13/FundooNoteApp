using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int LableId { get; set; }
            public string LabelName { get; set; } = string.Empty;

            // Foreign Key - Each label belongs to a user
            
            public int UserId { get; set; }
            public UserEntity User { get; set; } = null!;

            // Many-to-Many Relationship with Notes
            public ICollection<NoteLabelEntity> NoteLabel { get; set; } = new List<NoteLabelEntity>();
      






    }
}