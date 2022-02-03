using System;
using System.Collections.Generic;
using System.Text;

namespace FundooModels
{
    public class UpdateModel
    {
        public string Title { get; set; }

        public string Note { get; set; }

        public string Colour { get; set; }

        public string Image { get; set; }

        public bool Archive { get; set; }

        public bool Delete { get; set; }

        public bool Pin { get; set; }

        public bool Trash { get; set; }

        public DateTime Remainder { get; set; }
    }
}
