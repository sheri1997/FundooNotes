using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class CollaboratorEntity
    {
        [Key]
        public int CollabId { get; set; }

        [ForeignKey("NoteId")]
        public virtual NotesEntity Note { get; set; }

        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
