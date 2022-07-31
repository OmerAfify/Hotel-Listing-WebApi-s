using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.DTOs
{

    public class LoginUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserDTO : LoginUserDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual IEnumerable<string> roles  { get; set; }

    }
}
