using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class LabelEntity
    {
        [Key]
        public int LabelId { get; set; }

        [ForeignKey("NoteId")]
        public virtual NotesEntity Note { get; set; }


        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }

        [Required]
        public string LabelName { get; set; }
    }
}
