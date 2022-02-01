using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModels
{
    public class UserLogin //this class will be used for the login details from the database
    {
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid Email Format")] //Email sholud be in the given format only.
        [DataType(DataType.Text)]
        public string Email { get; set; } //email from the database 

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Invalid Password Entry Parameters")] //Password should be minimnum 8 and maximum 15 characters, should contain atleast one upeer case, one lower case and one special character.
        [DataType(DataType.Password)] //password from the database.
        public string Password { get; set; }
    }
}
