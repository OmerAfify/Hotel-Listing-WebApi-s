using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.DTOs
{

    public class LoginUserDto
    {
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
