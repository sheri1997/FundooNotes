using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModels
{
    public class UserModel //this class is used to get and set up the columns in the database
    {
        [Key]//setting the UserId as the primary key
        public int UserId { get; set; }
        
        [Required] //cannot be left empty.
        [RegularExpression(@"^[A-Za-z''-'\s]$",ErrorMessage ="Characters are Not Allowed")] //First name sholud be in the given format only.
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required] //cannot be left empty.
        [RegularExpression(@"^[A-Za-z''-'\s]$", ErrorMessage = "Characters are Not Allowed")] //Last name sholud be in the given format only.
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required] //cannot be left empty.
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage ="Invalid Email Format")] //Email sholud be in the given format only.
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required] //cannot be left empty.
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage ="Invalid Password Entry Parameters")] //Password should be minimnum 8 and maximum 15 characters, should contain atleast one upeer case, one lower case and one special character.
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
