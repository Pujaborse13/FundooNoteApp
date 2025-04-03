using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash {  get; set; }
        public DateTime CreatedAt {  get; set; }
        public DateTime tUpdatedAt { get; set; }

        [ForeignKey("NoteUser")]
        public int UserId { get; set; }
        
        [JsonIgnore]
        public virtual UserEntity NoteUser {  get; set; }

        //for many to many realtion-- adding later while creating label
        public ICollection<NoteLabelEntity> NoteLabel { get; set; } = new List<NoteLabelEntity>();




    }
}
