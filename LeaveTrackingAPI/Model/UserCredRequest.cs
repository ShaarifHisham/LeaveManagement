using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveTrack.Model
{
    public class UserCredRequest
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(15, ErrorMessage = "User Name Cannot Exceed more than 15 Characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$",
            ErrorMessage = "Password must be between 6 to 15 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
