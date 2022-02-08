using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModels
{
    public class ColloboratorModel
    {
        [Key]
        public int CollabId { get; set; }
        public string SenderEmail { get; set; }
        public string RecieverEmail { get; set; }
        public int NoteId { get; set; }
        public int UserId { get; set; }
    }
}
