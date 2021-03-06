using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class NotesEntity
    {
        [Key]
        public int NoteId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public string Colour { get; set; }

        public string Image { get; set; }

        public bool Archive { get; set; }

        public bool Delete { get; set; }

        public bool Pin { get; set; }

        public bool Trash { get; set; }
        
        public DateTime Remainder { get; set; }

        public virtual ICollection<LabelEntity> Lable { get; set; }

    }
}
